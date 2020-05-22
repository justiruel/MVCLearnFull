using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCLearn.Models.Dto;
using MVCLearnFull.Models;

namespace MVCLearnFull.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SessionName _sessionName;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _sessionName = new SessionName();
        }

        public IActionResult Index()
        {
            List<string> data = new List<string>
            {
                "data1",
                "data2"
            };
            ViewBag.Message = data;
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.UserName = HttpContext.Session.GetString(_sessionName.SessionUserName);
            return View();
        }

        public IActionResult SignIn(int id, int parambebas) //?parambebas=[value] //kalau khusus id jadinya /[value]
        {
            Console.WriteLine(parambebas); //output-> ASP.NET Core Web Server
            Console.WriteLine(id);
            if (id == 2)
            {
                return Redirect("/Home/Privacy");
            }
            else {
                return View();
            }

            
            
        }
        [HttpPost]
        public string SignIn(SignIn sg)
        {
            if (!String.IsNullOrEmpty(sg.Username) && !String.IsNullOrEmpty(sg.Password)) {
                HttpContext.Session.SetString(_sessionName.SessionUserName, sg.Username);

                //TODO: Save the data in database  
                return "welcome " + sg.Username;
            }
            else
                return "wrong username/password";
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home/Privacy");
        }
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
