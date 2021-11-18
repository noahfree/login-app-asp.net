using exploration3.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exploration3.Controllers
{
    public class NewUserController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "hsCW6M9h7SSd5w2xAgfLayIPxRs88452oMzyuPtk",
            BasePath = "https://exploration3-aspdotnet-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        public IActionResult Index()
        {
            CurrentUser.user = null;
            return View("NewUser");
        }

        public IActionResult ToLogin()
        {
            return Redirect("../Login");
        }

        private List<UserModel> GetUsers()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Users");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<UserModel>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<UserModel>(((JProperty)item).Value.ToString()));
            }
            return list;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserModel userModel)
        {
            try
            {
                if (!VerifyUser(userModel))
                {
                    ModelState.AddModelError(string.Empty, "Username is taken.");
                    return View("NewUser");
                }
                userModel.tries = 0;
                userModel.score = 0;
                userModel.time = new TimeSpan();
                AddUserToFirebase(userModel);
                ModelState.AddModelError(string.Empty, "Added successfully.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("NewUser");
            }
            CurrentUser.user = userModel;
            return Redirect("../Home");
        }

        private bool VerifyUser(UserModel userModel)
        {
            var list = GetUsers();
            foreach (var user in list)
            {
                if (userModel.Username.Equals(user.Username))
                {
                    ViewBag.ShowValidationError = true;
                    return false;
                }
            }
            return true;
        }

        private void AddUserToFirebase(UserModel userModel)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = userModel;
            PushResponse response = client.Push("Users/", data);
            data.ID = response.Result.name;
            client.Set("IDs/" + data.Username, data.ID);
            SetResponse setResponse = client.Set("Users/" + data.ID, data);
        }
    }
}
