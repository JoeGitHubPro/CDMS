using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.DAL.Data;
using System.Diagnostics;
using System.MVC.Models;
using System.MVC.ViewModels;

namespace System.MVC.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            FillReports();
            RetriveActivitiesDate();

            Counters();
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

            // Set TempData for immediate use if needed
            TempData["SelectedTheme"] = null;

            //Refresh page
            return Redirect(Request.Headers["Referer"].ToString());

        }
        public IActionResult SetTheme(string theme)
        {

            // Set TempData for immediate use if needed
            TempData["SelectedTheme"] = theme;
            //Refresh page
            return Redirect(Request.Headers["Referer"].ToString());

        }
        private void FillReports()
        {
            var result = _context.Sponsorships.Select(a => new { a.SponsorshipID, a.Date }).ToList();


            string CustodyList = string.Empty;
            string CustodyDateList = string.Empty;

            foreach (var item in result.Select(a => a.SponsorshipID))
            {
                CustodyList += $"{item},";
            }
            CustodyList.TrimEnd(',');

            foreach (var item in result.Select(a => a.Date))
            {
                CustodyDateList += $"{item},";
            }
            CustodyDateList.TrimEnd(',');

            ViewData["Custody"] = $"[{CustodyList}]";
            ViewData["CustodyDate"] = $"[{CustodyList}]";
        }
        private void RetriveActivitiesDate()
        {
            var result = _context.Activities.OrderByDescending(a => a.time).ToList();
            List<ActivityViewModel> model = new List<ActivityViewModel>();
            foreach (var item in result)
            {
                model.Add(new ActivityViewModel { Id = item.Id, Content = item.Content, time = item.time.Humanize() });
            }
            ViewBag.Activities = model;

        }
        private void Counters()
        {
            ViewBag.Users = _context.Users.Count();
            ViewBag.Devices = _context.Devices.Count();
            ViewBag.Custodies = _context.Sponsorships.Count();
            ViewBag.Locations = _context.Locations.Count();
            ViewBag.Categories = _context.DeviceCategories.Count();
        }
    }
}
