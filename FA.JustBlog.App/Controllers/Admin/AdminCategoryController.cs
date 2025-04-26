using Application.Dto.CategoryDtos;
using FA.JustBlog.App.Config;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace FA.JustBlog.App.Controllers.Admin
{
    public class AdminCategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminCategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<IndexCategoryDto>>(CommonUrl.CATEGORY_GET_ALL);
            return View(categories);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Console.WriteLine(CommonUrl.CATEGORY_GET_FOR_UPDATE(id));
            var category = await _httpClient.GetFromJsonAsync<UpdateCategoryDto>(CommonUrl.CATEGORY_GET_FOR_UPDATE(id));
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryDto updateCategoryDto)
        {
            if(ModelState.IsValid)
            {
                string? token;
                Request.Cookies.TryGetValue("Jwt", out token);
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var result = await _httpClient.PutAsJsonAsync(CommonUrl.CATEGORY_ADMIN, updateCategoryDto);
                return RedirectToAction("Index");
            }
            return View(updateCategoryDto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
        {
            if(ModelState.IsValid)
            {
                string? token;
                Request.Cookies.TryGetValue("Jwt", out token);
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var result = await _httpClient.PostAsJsonAsync(CommonUrl.CATEGORY_ADMIN, createCategoryDto);
                return RedirectToAction("Index");
            }
            return View(createCategoryDto);
        }

        //delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string? token;
            Request.Cookies.TryGetValue("Jwt", out token);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Console.WriteLine(CommonUrl.CATEGORY_ADMIN + $"/{id}");
            var result = await _httpClient.DeleteAsync(CommonUrl.CATEGORY_ADMIN + $"/{id}");
            return RedirectToAction("Index");
        }
    }
}
