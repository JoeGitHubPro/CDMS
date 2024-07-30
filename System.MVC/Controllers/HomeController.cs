using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.MVC.Models;

namespace System.MVC.Controllers
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
        public IActionResult SetDefaultTheme()
        {
            // Set the cookie for the selected theme
            //Response.Cookies.Append("SelectedTheme", null, new CookieOptions
            //{
            //    Expires = DateTime.Now.AddDays(30),
            //    Path = "/"
            //});

            // Set TempData for immediate use if needed
            TempData["SelectedTheme"] = null;

            //Refresh page
            return Redirect(Request.Headers["Referer"].ToString());

        }
        public IActionResult SetTheme(string theme)
        {

            // Set the cookie for the selected theme
            //Response.Cookies.Append("SelectedTheme", theme, new CookieOptions
            //{
            //    Expires = DateTime.Now.AddDays(30),
            //    Path = "/"
            //});

            // Set TempData for immediate use if needed
            TempData["SelectedTheme"] = theme;
            //Refresh page
            return Redirect(Request.Headers["Referer"].ToString());

        }
    }
}
