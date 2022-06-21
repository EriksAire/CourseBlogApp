using Application.Interfaces;
using Domain.Models;
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

        public HomeApiController(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await postService.GetAllPosts();
        }
    }
}
