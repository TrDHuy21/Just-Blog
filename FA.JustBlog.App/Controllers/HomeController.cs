using System.Diagnostics;
using FA.JustBlog.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.App.Controllers
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
            //return View();
            return Redirect("/post/page/1");
        }

        public IActionResult ParitalAboutCard()
        {
            return PartialView("_ParitalAboutCard");
        }

        public IActionResult Demo()
        {
            return View();
        }
    }
}
