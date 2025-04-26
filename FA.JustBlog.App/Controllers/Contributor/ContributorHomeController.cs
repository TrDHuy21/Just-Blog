    using System.Security.Claims;
using Application.Dto.CategoryDtos;
using Application.Dto.UserDtos;
using FA.JustBlog.App.Config;
using FA.JustBlog.App.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FA.JustBlog.App.Controllers.Contributor
{
    public class ContributorHomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public ContributorHomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            //var claims = CookieHelper.GetClaims(Request);
            //var id = Convert.ToInt16(claims.Find(c => c.Type == "nameid")?.Value);
            //var user = await _httpClient.GetFromJsonAsync<UserDto>(CommonUrl.USER_GET_BY_ID(id));
            //ViewBag.User = user;

            return RedirectToAction("Index", "Post");
        }
    }
}
