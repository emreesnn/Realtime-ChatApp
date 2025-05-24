using ChatApp.Dtos;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChatApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto userDto) {
            
            User user = new User()
            {
                Name = userDto.Name,
                Password = userDto.Password
            };

            if (await _userService.CreateUserAsync(user))
            {
                return Ok(user);
            }

            return BadRequest();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(UserDto userDto) {

            User user = await _userService.GetByNameAsync(userDto.Name);

            if (user.Password == userDto.Password)
            {
                return Ok(user);
            }
            else
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı!");
            }
        }
    }
}
