using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MVCLearn.Models.Dto;
using MVCLearnFull.Models;
using Newtonsoft.Json;
using TodoApi.Models;

namespace MVCLearnFull.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SessionName _sessionName;
        private readonly TodoContext _context;
        private IConfiguration _appSettings;

        public HomeController(ILogger<HomeController> logger, TodoContext context, IConfiguration configuration)
        {
            _logger = logger;
            _sessionName = new SessionName();
            _context = context;
            _appSettings = configuration;
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


        public IActionResult FlashData()
        {
            TempData["FlashData"] = "<b>Halo ini message dari tempdata kalau di php / code igniter nyebutnya flash data,<br/> " +
                                    "yaitu data yang hanya muncul sekali saat awal load, <br/>" +
                                    "untuk membuktikan silakan refresh halaman ini maka message akan hilang;</b>";
            return Redirect("/Home/FlashDataShow");
        }

        public IActionResult FlashDataShow()
        {
            _logger.LogInformation("Information");
            _logger.LogWarning("warning"); //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1

            try
            {
                Console.WriteLine("ChildData {0}", _appSettings.GetValue<string>("ChildData"));
                Console.WriteLine("ChildData2 {0}", _appSettings["ParentData:ChildData2"]);

            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }

            return View();
        }

        public ActionResult  /*Task<ActionResult<IEnumerable<TodoItem>>>*/ DataTable(int rwaId = 0)
        {
            return View();
        }

        [HttpPost]
        public string/*async Task<string>*/ DataTablePost(string sEcho, int iDisplayStart_offset = 0, int iDisplayLength_limit = 1000, string sSearch = null)
        {
            int totalRecord = _context.TodoItems.Count();
            //Console.WriteLine(sEcho);
            //var todo = await _context.TodoItems.ToListAsync();
            var todo = new List<TodoItem>();

            if (!string.IsNullOrEmpty(sSearch))
                todo = _context.TodoItems.Where(a => a.Title.ToLower().Contains(sSearch)
                //|| a.PercentageComplete.ToLower().Contains(sSearch)
                ).OrderBy(a => a.Id).Skip(iDisplayStart_offset).Take(iDisplayLength_limit).ToList();
            else
                todo = _context.TodoItems.OrderBy(a => a.Id).Skip(iDisplayStart_offset).Take(iDisplayLength_limit).ToList();

            var result = (from d in todo
                          select new GetTodoDto
                          {
                            Id = d.Id,
                            Title = d.Title,
                            ExpirateDate = d.ExpirateDate,
                            Description =d.Description,
                            PercentageComplete =d.PercentageComplete
                          }).ToList();

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append("\"sEcho\": "); //what is secho? https://datatables.net/forums/discussion/2087/secho-the-mystery-variable
            sb.Append(sEcho);
            sb.Append(",");
            sb.Append("\"iTotalRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"iTotalDisplayRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"aaData\": ");
            sb.Append(JsonConvert.SerializeObject(result));
            sb.Append("}");
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
