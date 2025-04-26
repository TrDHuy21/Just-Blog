using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using Application.Dto.UserDtos;
using FA.JustBlog.App.Config;
using FA.JustBlog.App.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FA.JustBlog.App.Controllers
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

                    // Giải mã token để lấy thông tin vai trò
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(result.Token);
                    var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

                    // Kiểm tra vai trò và điều hướng tương ứng
                    if (roleClaim == "Admin")
                    {
                        return RedirectToAction("Index", "AdminHome");
                    }
                    else if(roleClaim == "Contributor")
                    {
                        return RedirectToAction("Index", "ContributorHome");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("Jwt");
            }
            catch { }
            
            return RedirectToAction("Index", "Home");
        }
    }
}
