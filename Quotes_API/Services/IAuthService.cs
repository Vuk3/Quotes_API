using Quotes_API.Dtos;

namespace Quotes_API.Services
{
    public interface IAuthService
    {
        string GenerateTokenString(LoginUserDto user);
        Task<bool> Login(LoginUserDto user);
        Task<bool> RegisterUser(LoginUserDto user);
    }
}