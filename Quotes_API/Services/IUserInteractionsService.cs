using Quotes_API.Models;

namespace Quotes_API.Services
{
    public interface IUserInteractionsService
    {
        Task<List<UserAndQuote>> GetUserInteractions(string userName);
    }
}
