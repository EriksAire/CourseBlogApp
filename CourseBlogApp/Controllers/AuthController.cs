using Application.Interfaces;
using BlogAppAPI.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppAPI.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api")]
    public class AuthController : Controller    //TODO: Extract logic from controllers to services
    {
        private readonly IUnitOfWork _repository;
        private readonly IJwtService _jwtService;

        public AuthController(IUnitOfWork repository, IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            _repository.Repo<User>().Add(user);
            await _repository.Repo<User>().SaveChangesAsync();

            return Created("success", user);
        }


        //TODO: CHECK WHY RESPONSE.COOKIE.APPEND doesnt work on Chrome!!!!!!!
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _repository.Repo<User>().Find(s=>s.Email==dto.Email).FirstOrDefault();

            if (user == null) return BadRequest(new { message = "Invalid Credentials" });

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddMinutes(15),
                IsEssential = true,
                HttpOnly = true,
                Secure = true
            });
           
            return Ok(new
            {
                message = "success"
            });
        }

        [HttpGet("user")]
        public async Task<IActionResult> User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var user = await _repository.Repo<User>().GetByIdAsync(userId);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                SameSite = SameSiteMode.None,
                IsEssential = true,
                HttpOnly = true,
                Secure = true
            });

            return Ok(new
            {
                message = "success"
            });
        }
    }
}
