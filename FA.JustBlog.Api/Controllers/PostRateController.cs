using Application.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostRateController : ControllerBase
    {
        private readonly IPostRateService _postRateService;
        public PostRateController(IPostRateService postRateService)
        {
            _postRateService = postRateService;
        }

        [HttpPut("rate/{postId}")]
        [Authorize]
        public async Task<IActionResult> RatePost(int postId, decimal point)
        {
            var result = await _postRateService.RatePost(postId, point);
            return Ok(result); 
        }

    }
}
