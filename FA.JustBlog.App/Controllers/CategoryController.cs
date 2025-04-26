using Application.Dto.CategoryDtos;
using FA.JustBlog.App.Config;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> PartialMenu()
        {
            try {

                var categories = await _httpClient.GetFromJsonAsync<List<IndexCategoryDto>>(CommonUrl.CATEGORY_GET_ALL);
                return PartialView("_PartialMenu", categories);
            } catch
            {
                return BadRequest();
            }
        }
    }
}
