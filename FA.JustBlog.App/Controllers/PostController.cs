using System.Net.Http;
using System.Net.Http.Json;
using Application.Dto.PageDtos;
using Application.Dto.PostDtos;
using FA.JustBlog.App.Config;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.App.Controllers
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
            var ports = await _httpClient.GetFromJsonAsync<List<IndexPostDto>>(CommonUrl.POST_GET_ALL_FOR_USER);
            if (ports != null)
            {
                ports = ports.OrderByDescending(p => p.CreatedDate).ToList();
            }
            return View(ports);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var post = await _httpClient.GetFromJsonAsync<DetailPostDto>(CommonUrl.POST_GET_BY_ID(id));
            return View("~/Views/Post/Detail.cshtml", post);
        }

        [ActionName("FindPost")]
        public async Task<IActionResult> FindPost(int year, int month, string title)
        {
            var posts = await _httpClient.GetFromJsonAsync<DetailPostDto>(CommonUrl.POST_FIND(year, month, title));
            return View("Detail", posts);
        }

        public async Task<IActionResult> FindByTagName(string tagName)
        {
            Console.WriteLine("Helooo" +  CommonUrl.POST_GET_BY_TAG(tagName));
            var posts = await _httpClient.GetFromJsonAsync<List<IndexPostDto>>(CommonUrl.POST_GET_BY_TAG(tagName));
            return View("Index", posts);
        }

        public async Task<IActionResult> FindByCategory(int id)
        {
            var posts = await _httpClient.GetFromJsonAsync<List<IndexPostDto>>(CommonUrl.POST_GET_BY_CATEGORY(id));
            return View("Index", posts);
        }

        public async Task<IActionResult> GetTopView(int top = 5)
        {
            var posts = await _httpClient.GetFromJsonAsync<List<IndexPostDto>>(CommonUrl.POST_GET_TOP_VIEW(top));
            return PartialView("_ListPosts", posts);
        }

        [HttpGet("/post/page/{index}")]
        [ActionName("IndexPostPage")]
        public async Task<IActionResult> IndexPage(int index=1)
        {
            var pageResultDto =  await _httpClient.GetFromJsonAsync<PageResultDto<IndexPostDto>>(CommonUrl.POST_GET_BY_PAGE(index));
            return View("IndexPage", pageResultDto);
        }

       
    }
}
