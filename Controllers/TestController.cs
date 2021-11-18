using exploration3.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exploration3.Controllers
{
    public class TestController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "hsCW6M9h7SSd5w2xAgfLayIPxRs88452oMzyuPtk",
            BasePath = "https://exploration3-aspdotnet-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        public IActionResult Index(TestModel testModel)
        {
            if (CurrentUser.user == null)
            {
                return Redirect("../Login");
            }
            Numbers.numbers = new int[10];
            testModel.numbers = new int[10];
            Random r = new Random();
            for (int i = 0; i < testModel.numbers.Length; i++)
            {
                Numbers.numbers[i] = r.Next(10, 100);
                testModel.numbers[i] = Numbers.numbers[i];
            }
            ViewBag.Numbers = testModel.numbers;
            return View("Test");
        }

        public IActionResult Submit(TestModel testModel)
        {
            if (CurrentUser.user == null)
            {
                return Redirect("../Login");
            }
            Numbers.endTime = DateTime.Now;
            int count = 0; 
            int[] inputs = { testModel.input1, testModel.input2, testModel.input3, testModel.input4, testModel.input5 };
            int[] answers = new int[5];
            int i, j;
            for (i = j = 0; i < 10; i += 2)
            {
                answers[j] = Numbers.numbers[i] * Numbers.numbers[i + 1];
                if (answers[j] == inputs[j])
                {
                    count++;
                }
                j++;
            }
            ViewBag.Correct = count;
            ViewBag.Answers = answers;
            ViewBag.Numbers = Numbers.numbers;
            var time = ViewBag.Time = (Numbers.endTime - Numbers.startTime);
            if (count > CurrentUser.user.score)
            {
                CurrentUser.user.score = count;
                CurrentUser.user.time = time;
            }
            else if (count == CurrentUser.user.score && time < CurrentUser.user.time)
            {
                CurrentUser.user.time = time;
            }
            CurrentUser.user.tries++;
            Edit(CurrentUser.user);
            return View("Answers");
        }

        [HttpGet]
        public void Detail(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Users/" + id);
            UserModel data = JsonConvert.DeserializeObject<UserModel>(response.Body);
        }

        [HttpPost]
        public void Edit(UserModel user)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse response = client.Set("Users/" + user.ID.Replace("\"", "") + "/score", user.score);
            response = client.Set("Users/" + user.ID.Replace("\"", "") + "/tries", user.tries);
            response = client.Set("Users/" + user.ID.Replace("\"", "") + "/time", user.time);
        }

        public IActionResult Home()
        {
            return Redirect("../Home");
        }
    }
}
