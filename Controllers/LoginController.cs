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
    public class LoginController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "hsCW6M9h7SSd5w2xAgfLayIPxRs88452oMzyuPtk",
            BasePath = "https://exploration3-aspdotnet-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        public IActionResult Index()
        {
            //TempData["currentUser"] = null;
            CurrentUser.user = null;
            return View("Login");
        }

        private List<UserModel> GetUsers()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Users");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<UserModel>();
            int count = 0;
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<UserModel>(((JProperty)item).Value.ToString()));
                count++;
            }
            return list;
        }


        public IActionResult NewUser()
        {
            return Redirect("../NewUser");
        }
        

        public IActionResult Login(UserModel userModel)
        {
            var list = GetUsers();
            foreach(var user in list)
            {
                if (userModel.Username.Equals(user.Username) && userModel.Password.Equals(user.Password))
                {
                    ViewBag.ShowValidationError = false;
                    CurrentUser.user = user;

                    CurrentUser.user.ID = client.Get("IDs/" + CurrentUser.user.Username).Body.ToString();
                    return Redirect("../Home");
                }
            }
            ViewBag.ShowValidationError = true;
            return View("Login");
        }
    }
}
