using System.Net.Http.Json;
using Application.Dto.CategoryDtos;
using Application.Dto.PageDtos;
using Application.Dto.PostDtos;
using Azure;
using Domain.Entity;
using FA.JustBlog.App.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FA.JustBlog.App.Controllers.Admin
{
    public class AdminPostController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminPostController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(int index = 1)
        {
            string? token;
            Request.Cookies.TryGetValue("Jwt", out token);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var posts = await _httpClient.GetFromJsonAsync<List<IndexPostDto>>(CommonUrl.POST_ADMIN);
            var PageResultDto = await _httpClient.GetFromJsonAsync<PageResultDto<IndexPostDto>>(CommonUrl.POST_GET_BY_PAGE(index));

            return View(PageResultDto);
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
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest("you are not owner, you can not edit this post");
                }
                
            }
            return View(updatePostDto);
        }

        public async Task<IActionResult> Create()
        {
            // get all category
            string? token;
            Request.Cookies.TryGetValue("Jwt", out token);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var categories = await _httpClient.GetFromJsonAsync<List<IndexCategoryDto>>(CommonUrl.CATEGORY_GET_ALL);
            ViewBag.Categories = categories.Select(c => new SelectListItem
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
                    return RedirectToAction("Index");
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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string? token;
            Request.Cookies.TryGetValue("Jwt", out token);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await _httpClient.DeleteAsync(CommonUrl.POST_ADMIN + $"/{id}");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Publish(int id)
        {
            string? token;
            Request.Cookies.TryGetValue("Jwt", out token);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await _httpClient.PutAsync(CommonUrl.POST_PUBLISH(id), new StringContent(string.Empty));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetPostByFilter(int index = 1,string sapxep = "lastest",string ispublish = "all")
        {
            var pageResultDto = await _httpClient.GetFromJsonAsync<PageResultDto<IndexPostDto>>(CommonUrl.POST_FILTER(sapxep, ispublish, index, 10));
            ViewBag.SapXep = sapxep;
            ViewBag.IsPublish = ispublish;

            return View("Index", pageResultDto);
        }
    }
}
