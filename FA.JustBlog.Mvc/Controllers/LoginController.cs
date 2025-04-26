using System.Net.Http.Json;
using Application.Dto.UserDtos;
using FA.JustBlog.Mvc.Config;
using FA.JustBlog.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FA.JustBlog.Mvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                // Gửi yêu cầu POST với loginDto
                var response = await _httpClient.PostAsJsonAsync(CommonUrl.LOGIN_URL_GET_TOKEN, loginDto);

                // Kiểm tra xem yêu cầu có thành công không
                if (response.IsSuccessStatusCode)
                {
                    // Nhận token từ phản hồi
                    var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                    var cookieOptions = new CookieOptions();
                    Response.Cookies.Append("Jwt", result.Token, cookieOptions);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View();
                }
            }

            return RedirectToAction("Index", "Post");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Jwt");
            return RedirectToAction("Index", "Home");
        }
    }
}
