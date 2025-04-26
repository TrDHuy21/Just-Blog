using Application.Dto.CategoryDtos;
using Application.Dto.PostDtos;
using Application.Service.Implementation;
using Application.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminPostController : ControllerBase
    {
        private readonly IPostService _postService;

        public AdminPostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllPostAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getPostForUpdate(int id)
        {
            var post = await _postService.GetPostForUpdateAsync(id);
            return Ok(post);
        }

        [HttpPut]
        public async Task<IActionResult> updateCategory(UpdatePostDto updatePostDto)
        {
            var result = await _postService.UpdatePostAsync(updatePostDto);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(CreatePostDto createPostDto)
        {
            var result = await _postService.AddPostAsync(createPostDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _postService.DeletePostAsync(id);
            return Ok();
        }

        [HttpPut]
        [Route("PublishPost/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PublishPost(int id)
        {
            var result = await _postService.PublishPostAsync(id);
            return Ok();
        }


    }
}
