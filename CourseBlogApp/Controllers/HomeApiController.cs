using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly ICommentService commentService;

        public HomeApiController(IPostService postService, ICommentService commentService)
        {
            this.postService = postService;
            this.commentService = commentService;
        }

        [HttpGet]
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

        public async Task DeletePost(int id)
        {
            await postService.DeletePost(id);
        }


        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult> GetComments()
        {
            var commentsList = await postService.GetAllPosts();

            if (commentsList != null)
            {
                return Ok(commentsList);
            }

            return NoContent();
        }

        [Authorize]
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> GetComments(int id)
        {
            var comment = await commentService.GetComment(id);

            if (comment != null)
            {
                return Ok(comment);
            }

            return NoContent();
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task AddComment([FromBody] Comment comment)
        {
            await commentService.AddComment(comment);
        }

        [Authorize]
        [HttpPut("[action]/{id}")]
        public async Task<ActionResult> EditComment(int id, [FromBody] Comment comment)
        {
            if (id != comment.ID)
            {
                return BadRequest();
            }
            await commentService.EditComment(id, comment);
            return Ok();
        }

        public async Task DeleteComment(int id)
        {
            await commentService.DeleteComment(id);
        }
    }
}
