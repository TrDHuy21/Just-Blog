using Application.Dto.CommentDtos;
using Application.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FA.JustBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto createCommentDto)
        {
            var result = await _commentService.CreateComment(createCommentDto);
            return Ok(result);
        }

        [HttpDelete("{commentId}")]
        [Authorize]

        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var result = await _commentService.DeleteComment(commentId);
            return Ok(result);
        }
    }
}
