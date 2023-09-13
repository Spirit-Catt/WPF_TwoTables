using CoreMVC_WebAPI.Data;
using CoreMVC_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CoreMVC_WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PubsContext _context;

        public HomeController(ILogger<HomeController> logger, PubsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Authors()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
                id = "555-888-999";

            var au = (from a in _context.Authors
                      where a.au_id == id
                      select a).FirstOrDefault();

            if (au == null)
                return RedirectToAction("Index", "Au");

            return View("EditAuthor", au);
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