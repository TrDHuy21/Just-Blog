using Application.Dto.PostDtos;
using Application.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FA.JustBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        protected readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForUser()
        {
            var posts = await _postService.GetAllPostForUserAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            return Ok(post);
        }

        [HttpGet("tag/{tagName}")]
        public async Task<IActionResult> GetPostByTag(string tagName)
        {
            List<IndexPostDto> posts = await _postService.GetPostByTagForUserAsync(tagName);
            return Ok(posts);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetPostByCategory(int id)
        {
            List<IndexPostDto> posts = await _postService.GetPostByCategoryForUserAsync(id);
            return Ok(posts);
        }

        [HttpGet("mypost")]
        public async Task<IActionResult> GetMyPost()
        {
            var posts = await _postService.GetMyPostAsync();
            return Ok(posts);
        }

        [HttpGet("topview/{top}")]
        public async Task<IActionResult> GetTopView(int top = 5)
        {
            var posts = await _postService.GetTopViewAsync(top);
            return Ok(posts);
        }

        [HttpGet("{year}/{month}/{title}")]
        public async Task<IActionResult> FindPostx(int year, int month, string title)
        {
            var post = await _postService.FindPost(year, month, title);
            return Ok(post);
        }

        [HttpGet("page/{index}")]
        public async Task<IActionResult> GetPostPage(int index = 1, int pageSize = 10)
        {
             var posts = await _postService.GetPostPageAsync(index, pageSize);
            return Ok(posts);
        }

        [HttpGet("filter/{index}")]
        public async Task<IActionResult> GetPostByFilter([FromQuery] string sapxep, [FromQuery] string ispublish="all", int index = 1, int pageSize = 10)
        {
            var pageResultDto = await _postService.GetPostByFilterAsync(sapxep, ispublish, index, pageSize);
            return Ok(pageResultDto);
        }
    }
}
