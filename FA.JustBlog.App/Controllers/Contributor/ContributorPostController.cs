using Application.Dto.CategoryDtos;
using Application.Dto.PostDtos;
using FA.JustBlog.App.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FA.JustBlog.App.Controllers.Contributor
{
    public class ContributorPostController : Controller
    {
        private readonly HttpClient _httpClient;

        public ContributorPostController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetMyPost(int id)
        {
            string? token;
            Request.Cookies.TryGetValue("Jwt", out token);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var posts = await _httpClient.GetFromJsonAsync<List<IndexPostDto>>(CommonUrl.POST_GET_BY_USER);
            return View(posts);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<IndexCategoryDto>>(CommonUrl.CATEGORY_GET_ALL);
            ViewBag.Categories = categories?.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }
            ).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto createPostDto)
        {
            if (ModelState.IsValid)
            {
                string? token;
                Request.Cookies.TryGetValue("Jwt", out token);
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await _httpClient.PostAsJsonAsync(CommonUrl.POST_ADMIN, createPostDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetMyPost");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    // Xử lý lỗi, in ra nội dung lỗi
                    Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                }
            }
            var categories = await _httpClient.GetFromJsonAsync<List<IndexCategoryDto>>(CommonUrl.CATEGORY_GET_ALL);
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }
            ).ToList();
            return View(createPostDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            string? token;
            Request.Cookies.TryGetValue("Jwt", out token);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var post = await _httpClient.GetFromJsonAsync<UpdatePostDto>(CommonUrl.POST_GET_FOR_UPDATE(id));
            var categories = await _httpClient.GetFromJsonAsync<List<IndexCategoryDto>>(CommonUrl.CATEGORY_GET_ALL);
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }
            ).ToList();

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePostDto updatePostDto)
        {
            if (ModelState.IsValid)
            {
                string? token;
                Request.Cookies.TryGetValue("Jwt", out token);
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var result = await _httpClient.PutAsJsonAsync(CommonUrl.POST_ADMIN, updatePostDto);
                return RedirectToAction("GetMyPost");
            }
            return View(updatePostDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string? token;
            Request.Cookies.TryGetValue("Jwt", out token);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await _httpClient.DeleteAsync(CommonUrl.POST_ADMIN + $"/{id}");
            return RedirectToAction("GetMyPost");
        }
    }
}
