using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quotes_API.Dtos;
using Quotes_API.Models;
using Quotes_API.Services;
using System.Security.Claims;

namespace Quotes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        private readonly ITagService _tagService;

        public QuotesController(IQuoteService quoteService, ITagService tagService)
        {
            _quoteService = quoteService;
            _tagService = tagService;
        }

        [HttpGet("GetQuotes")]
        public async Task<ActionResult> GetQuotes(int page = 1, int pageSize = 5, string sort = "default", string? tags = null)
        {
            var tagsList = await _tagService.GetTagsByString(tags);

            var totalQuotes = await _quoteService.GetNumberOfQuotes(tagsList);

            var totalPages = (int)Math.Ceiling((double)totalQuotes / pageSize);
            if(totalPages == 0)
            {
                page = 0;
            }
            if(page>totalPages)
            {
                page = 1;
            }
            if(page==0 && totalPages>0)
            {
                page = 1;
            }
            var quotes = await _quoteService.GetQuotes(page, pageSize, sort, tagsList);

            return Ok(new { Quotes = quotes, TotalPages = totalPages, CurrentPage = page });
        }

        [HttpGet("GetAllQuotes")]
        public async Task<IActionResult> GetAllQuotes()
        {
            var quotes = await _quoteService.GetAllQuotes();
            return Ok(quotes);
        }

        [HttpPost("AddQuote")]
        public async Task<IActionResult> AddQuote(QuoteDto quoteDto)
        {
            var result = await _quoteService.AddQuote(quoteDto);
            if (result)
            {
                return Ok("Done");
            }
            return BadRequest();
        }

        [HttpPost("{quoteId}/like")]
        public async Task<IActionResult> LikeQuote(int quoteId)
        {
            var userName = User.FindFirst(ClaimTypes.Email)?.Value;
            
            if(userName == null)
            {
                return BadRequest();
            }

            var quote = await _quoteService.GetQuote(quoteId);

            var votes = await _quoteService.LikeQuote(quote, userName);

            return Ok(new { positiveVotes = votes.Item1, negativeVotes = votes.Item2 });
        }

        [HttpPost("{quoteId}/dislike")]
        public async Task<IActionResult> DislikeQuote(int quoteId)
        {
            var userName = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userName == null)
            {
                return BadRequest();
            }

            var quote = await _quoteService.GetQuote(quoteId);

            var votes = await _quoteService.DislikeQuote(quote, userName);

            return Ok(new { positiveVotes = votes.Item1, negativeVotes = votes.Item2 });
        }

        [HttpDelete("DeleteAllQuotes")]
        public async Task<ActionResult> DeleteAllQuotes()
        {
            var tagsDeleted = await _tagService.DeleteAllTags();
            var quotesDeleted = await _quoteService.DeleteAllQuotes();
            if(tagsDeleted && quotesDeleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
