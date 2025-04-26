using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.App.Controllers.Admin
{
    public class AdminHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
