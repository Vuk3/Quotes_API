using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quotes_API.Contexts;
using Quotes_API.Dtos;
using Quotes_API.Models;
using Quotes_API.Services;
using System.Security.Claims;

namespace Quotes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserInteractionsController : ControllerBase
    {
        private readonly IUserInteractionsService _userInteractionsService;
        public UserInteractionsController(IUserInteractionsService userInteractionsService)
        {
            _userInteractionsService = userInteractionsService;
        }

        [HttpGet("GetUserInteractions")]
        public async Task<ActionResult<List<UserAndQuoteDto>>> GetUserInteractions()
        {
            var userName = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userName == null)
            {
                return BadRequest();
            }

            var userInteractions = await _userInteractionsService.GetUserInteractions(userName);

            if(userInteractions == null)
            {
                return Ok();
            }


            var userInteractionsDto = userInteractions.Select(ui => new UserAndQuoteDto
            {
                PostId = ui.QuoteId,
                InteractionType = ui.Reaction
            }).ToList();

            return Ok(userInteractionsDto);
        }
    }
}
