using exploration3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace exploration3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (CurrentUser.user == null)
            {
                return Redirect("../Login");
            }
            ViewBag.User = CurrentUser.user.Username;
            return View();
        }

        public IActionResult Info()
        {
            if (CurrentUser.user == null)
            {
                return Redirect("../Login");
            }
            ViewBag.User = CurrentUser.user.Username;
            ViewBag.Tries = CurrentUser.user.tries;
            ViewBag.Score = CurrentUser.user.score;
            ViewBag.Time = CurrentUser.user.time;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Start()
        {
            Numbers.startTime = DateTime.Now;
            return Redirect("../Test");
        }
    }
}
