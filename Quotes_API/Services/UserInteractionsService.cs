using Microsoft.EntityFrameworkCore;
using Quotes_API.Contexts;
using Quotes_API.Models;

namespace Quotes_API.Services
{
    public class UserInteractionsService : IUserInteractionsService
    {
        private readonly AppDbContext _context;
        public UserInteractionsService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<List<UserAndQuote>> GetUserInteractions(string userName)
        {
            var interactions = await _context.UsersAndQuotes
            .Where(ui => ui.UserEmail == userName)
            .ToListAsync();

            return interactions;
        }
    }
}
