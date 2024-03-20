using Quotes_API.Dtos;
using Quotes_API.Migrations;
using Quotes_API.Models;

namespace Quotes_API.Services
{
    public interface IQuoteService
    {

        Task<bool> AddQuote(QuoteDto quoteDto);
        Task<List<Quote>> GetAllQuotes();
        Task<Quote> GetQuote(int quoteId);
        Task<(int, int)> LikeQuote(Quote quote, string userName);

        Task<(int, int)> DislikeQuote(Quote quote, string userName);
        Task<List<Quote>> GetQuotes(int page, int pageSize, string sort, List<Tag> tags);
        Task<int> GetNumberOfQuotes(List<Tag> tags);
        Task<List<Tag>> GetTags(QuoteDto quoteDto);

        Task<bool> DeleteAllQuotes();
    }
}