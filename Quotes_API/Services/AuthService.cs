using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Quotes_API.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Quotes_API.Dtos;

namespace Quotes_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<bool> RegisterUser(LoginUserDto userDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = userDto.UserName,
                Email = userDto.UserName
            };

            var result = await _userManager.CreateAsync(identityUser, userDto.Password);
            return result.Succeeded;
        }

        public async Task<bool> Login(LoginUserDto userDto)
        {
            var identityUser = await _userManager.FindByEmailAsync(userDto.UserName);
            if (identityUser == null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(identityUser, userDto.Password);
        }

        public string GenerateTokenString(LoginUserDto userDto)
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,userDto.UserName),
                new Claim(ClaimTypes.Role,"User"),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred
                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
