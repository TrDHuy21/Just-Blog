using System.Net.Http.Headers;
using System.Net.Http.Json;
using Application.Dto.PostDtos;
using FA.JustBlog.Mvc.Config;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Mvc.Controllers
{
    public class PostController : Controller
    {
        private readonly HttpClient _httpClient;

        public PostController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // get jwt from cookie
                string? token;
                Request.Cookies.TryGetValue("Jwt", out token);

                // assgin jwt to authencation header
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var posts = await _httpClient.GetFromJsonAsync<List<IndexPostDto>>(CommonUrl.POST_URL_GET_ALL);
                return View(posts);
            } catch
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var post = await _httpClient.GetFromJsonAsync<DetailPostDto>(CommonUrl.POST_URL_GET_DETAIL(id));
            return View(post);
        }
    }
}
