using Application.Dto.TagDtos;
using FA.JustBlog.App.Config;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.App.Controllers
{
    public class TagController : Controller
    {
        private readonly HttpClient _httpClient;
        public TagController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PopularTags(int top = 10)
        {
            Console.WriteLine("Da vao controller");
            var tags = await _httpClient.GetFromJsonAsync<List<TagDto>>(CommonUrl.TAG_GET_TOP(top));
            Console.WriteLine(tags.Count);
            return PartialView("_PopularTags", tags);
        }
    }
}
