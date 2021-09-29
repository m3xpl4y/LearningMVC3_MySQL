using LearningMVC3_MySQL.Models;
using LearningMVC3_MySQL.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LearningMVC3_MySQL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IHttpContextAccessor _httpContextAccessor { get; }

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string value = "darkMode";
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);

            _httpContextAccessor.HttpContext.Response.Cookies.Append("Theme", value, options);

            var valueOfThemeCookie = _httpContextAccessor.HttpContext.Request.Cookies["Theme"];
            var cookieValue = Request.Cookies["Theme"];

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
