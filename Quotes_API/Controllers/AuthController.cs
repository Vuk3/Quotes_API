using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quotes_API.Dtos;
using Quotes_API.Models;
using Quotes_API.Services;
using System.Security.Claims;

namespace Quotes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(LoginUserDto userDto)
        {
            if(await _authService.RegisterUser(userDto))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _authService.Login(userDto);
            if (result)
            {
                var tokenString = _authService.GenerateTokenString(userDto);
                return Ok(tokenString);
            }
            return BadRequest();
        }
    }
}
