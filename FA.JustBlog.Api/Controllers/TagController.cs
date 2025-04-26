using Application.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("toppopular/{top}")]
        public async Task<IActionResult> GetTopPopular(int top = 10)
        {
            Console.WriteLine("controller api GetTopPopular ");
            var tags = await _tagService.GetTopPopular(top);
            return Ok(tags);
        }
    }
}
