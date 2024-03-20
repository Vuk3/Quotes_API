using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Quotes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorizeController : ControllerBase
    {
        public AuthorizeController()
        {
            
        }

        [HttpGet("IsAuthorized")]
        public IActionResult IsAuthorized()
        {
            return Ok();
        }
    }
}
