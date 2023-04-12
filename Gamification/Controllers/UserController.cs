using Gamification.DTOs;
using Gamification.Entities;
using Gamification.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Gamification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration) {
            _userService = userService;
            _configuration = configuration;
        }



        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult> SignUp([FromBody] UserDTOs t)
        {
            
                var entity = new User
                {
                    Id = Guid.NewGuid(),
                    Email = t.Email,
                    Password = t.Password,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    IsAdmin = t.IsAdmin,
                    Type = t.Type,
                    
                };
                var user1 = await _userService.SignupAsync(entity);
                return Ok(user1);
            
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> Login([FromBody] LogInDTO loginDto)
        {
            try
            {
                var user = await _userService.LoginAsync(loginDto.Email, loginDto.Password);

                if (user == null)
                    return BadRequest("Invalid email or password");

                return Ok(user );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserDetails/{userId}")]
        public async Task<ActionResult<User>> GetUserDetails([FromRoute] Guid userId)
        {
            var user = await _userService.GetUserDetailsAsync(userId);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            var user = await _userService.GetAllUsersWithBadgesTokensQuestsAsync();

           
            return Ok(user.ToList());
        }



        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult<User>> GetUserById([FromRoute] Guid id)
        {
            var user = await _userService.FindById(id);

         
            return Ok(user);
        }



        private string GenerateJwtToken(UserDTOs user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              expires: DateTime.Now.AddDays(7),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
