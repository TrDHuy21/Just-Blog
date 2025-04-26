using Application.Dto.CommentDtos;
using FA.JustBlog.App.Config;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.App.Controllers
{
    public class CommentController : Controller
    {
        private readonly HttpClient _httpClient;

        public CommentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDto createCommentDto)
        {
            try
            {
                if (createCommentDto == null)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid)
                {
                    string? token;
                    Request.Cookies.TryGetValue("Jwt", out token);
                    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    var response = await _httpClient.PostAsJsonAsync(CommonUrl.COMMENT_URL, createCommentDto);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Detail", "Post", new { id = createCommentDto.PostId });
                    }
                   
                    return BadRequest("you have to login to comment");
                }
                return RedirectToAction("Detail", "Post", new { id = createCommentDto.PostId });
            }
            catch
            {
                return RedirectToAction("Detail", "Post", new { id = createCommentDto.PostId });
            }
        }
    }
}
