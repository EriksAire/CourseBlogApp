using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppAPI.Controllers
{
    //[Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        private readonly IPostService postService;

        public HomeApiController(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<IActionResult> GetPosts()
        {
            var postslist = await postService.GetAllPosts();

            if (postslist != null)
            {
                return Ok(postslist);
            }

            return NoContent();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosts(int id)
        {
            var post = await postService.GetPost(id);

            if(post != null)
            {
                return Ok(post);
            }

            return NoContent();
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task CreatePost([FromBody] Post post)
        {
            await postService.AddPost(post);
        }

        [Authorize]
        [HttpPut("[action]/{id}")]
        public async Task<ActionResult> EditPost(int id, [FromBody] Post post)
        {
            if (id != post.ID)
            {
                return BadRequest();
            }
            await postService.EditPost(id, post);
            return Ok();
        }
    }
}
