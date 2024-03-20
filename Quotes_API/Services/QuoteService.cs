using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quotes_API.Contexts;
using Quotes_API.Dtos;
using Quotes_API.Models;
using System.Security.Claims;

namespace Quotes_API.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly AppDbContext _context;
        public QuoteService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<Quote>> GetAllQuotes()
        {
            return await _context.Quotes
                .Include(p => p.ReactionsByUsers)
                .Include(p=>p.Tags)
                .ToListAsync();
        }

        public async Task<bool> DeleteAllQuotes()
        {
            try
            {
                _context.Quotes.RemoveRange(_context.Quotes);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AddQuote(QuoteDto quoteDto)
        {
            var listOfTags = new List<Tag>();
            foreach (var tagDto in quoteDto.Tags)
            {
                var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Value == tagDto.Value);

                if (existingTag == null)
                {
                    var newTag = new Tag { Value = tagDto.Value };
                    listOfTags.Add(newTag);
                    _context.Tags.Add(newTag);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    listOfTags.Add(existingTag);
                }
            }            


            var quote = new Quote
            {
                Text = quoteDto.Text,
                Author = quoteDto.Author,
                Tags = listOfTags
            };

            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Quote> GetQuote(int quoteId)
        {
            var quote = await _context.Quotes.FindAsync(quoteId);
            if (quote != null)
            {
                return quote;
            }
            return null;
        }

        public async Task<(int, int)> LikeQuote(Quote quote, string userName)
        {
            var UserAndQuote = _context.UsersAndQuotes
                .Where(p => p.QuoteId == quote.Id && p.UserEmail == userName).SingleOrDefault();

            if (UserAndQuote == null)
            {
                var UserAndQuoteObject = new UserAndQuote
                {
                    UserEmail = userName,
                    QuoteId = quote.Id,
                    Reaction = Constants.LikeValue
                };

                _context.UsersAndQuotes.Add(UserAndQuoteObject);
                quote.ReactionsByUsers.Add(UserAndQuoteObject);
                quote.PositiveVotes++;

            }
            else
            {
                if (UserAndQuote.Reaction == Constants.LikeValue)
                {
                    _context.UsersAndQuotes.Remove(UserAndQuote);
                    quote.PositiveVotes--;
                }
                else
                {
                    UserAndQuote.Reaction = Constants.LikeValue;
                    quote.PositiveVotes++;
                    quote.NegativeVotes--;
                }
            }
            await _context.SaveChangesAsync();
            return (quote.PositiveVotes, quote.NegativeVotes);
        }

        public async Task<(int, int)> DislikeQuote(Quote quote, string userName)
        {
            var UserAndQuote = _context.UsersAndQuotes
                .Where(p => p.QuoteId == quote.Id && p.UserEmail == userName).SingleOrDefault();

            if (UserAndQuote == null)
            {
                var UserAndQuoteObject = new UserAndQuote
                {
                    UserEmail = userName,
                    QuoteId = quote.Id,
                    Reaction = Constants.DislikeValue
                };

                _context.UsersAndQuotes.Add(UserAndQuoteObject);
                quote.ReactionsByUsers.Add(UserAndQuoteObject);
                quote.NegativeVotes++;

            }
            else
            {
                if (UserAndQuote.Reaction == Constants.DislikeValue)
                {
                    _context.UsersAndQuotes.Remove(UserAndQuote);
                    quote.NegativeVotes--;
                }
                else
                {
                    UserAndQuote.Reaction = Constants.DislikeValue;
                    quote.PositiveVotes--;
                    quote.NegativeVotes++;
                }
            }
            await _context.SaveChangesAsync();
            return(quote.PositiveVotes, quote.NegativeVotes);
        }

        public async Task<List<Quote>> GetQuotes(int page, int pageSize, string sort, List<Tag> tags)
        {
            if (page != 0)
            {
                IQueryable<Quote> quotesQuery = _context.Quotes;

                if (tags != null && tags.Count > 0)
                {
                    var tagValues = tags.Select(tag => tag.Value).ToList();
                    quotesQuery = quotesQuery.Where(q => q.Tags.Any(t => tagValues.Contains(t.Value)));

                }

                switch (sort.ToLower())
                {
                    case "asc":
                        quotesQuery = quotesQuery.OrderBy(q => (q.PositiveVotes + q.NegativeVotes) == 0 ? 0 : (double)q.PositiveVotes / (q.PositiveVotes + q.NegativeVotes));

                        break;
                    case "desc":
                        quotesQuery = quotesQuery.OrderByDescending(q => (q.PositiveVotes + q.NegativeVotes) == 0 ? 0 : (double)q.PositiveVotes / (q.PositiveVotes + q.NegativeVotes));
                        break;
                    default:
                        break;
                }
                var quotes = await quotesQuery
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return quotes;
            }
            else
            {
                return new List<Quote>();
            }

        }

        public async Task<int> GetNumberOfQuotes(List<Tag> tags)
        {
            if (tags == null)
            {
                return await _context.Quotes.CountAsync();
            }
            var tagValues = tags.Select(tag => tag.Value).ToList();
            var count = _context.Quotes
                .Where(q => q.Tags.Any(t => tagValues.Contains(t.Value)))
                .Count();


            return count;
        }

        public async Task<List<Tag>> GetTags(QuoteDto quoteDto)
        {
            var quoteWithTags = await _context.Quotes
                .Include(q => q.Tags)
                .FirstOrDefaultAsync(q => q.Id == quoteDto.Id);

            if (quoteWithTags != null)
            {
                return quoteWithTags.Tags;
            }
            else
            {
                return new List<Tag>();
            }
        }
    }
}
