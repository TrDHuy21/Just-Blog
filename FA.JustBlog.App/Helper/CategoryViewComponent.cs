using Application.Dto.CategoryDtos;
using FA.JustBlog.App.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FA.JustBlog.App.Helper
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly HttpClient _httpClient;
        public CategoryViewComponent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<IndexCategoryDto>>(CommonUrl.CATEGORY_GET_ALL);
            var categoryItems = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CategoryName
            }).ToList();

            return View(categoryItems);
        }
    }
}
