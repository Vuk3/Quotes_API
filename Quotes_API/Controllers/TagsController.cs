using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quotes_API.Dtos;
using Quotes_API.Models;
using Quotes_API.Services;

namespace Quotes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("AddTag")]
        public async Task<IActionResult> AddTag(TagDto tagDto)
        {
            var result = await _tagService.AddTag(tagDto);
            if (result)
            {
                return Ok("Done");
            }
            return BadRequest("Existing tag");
        }

        [HttpGet("GetAllTags")]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagService.GetAllTags();
            return Ok(tags);
        }

        [HttpDelete("DeleteAllTags")]
        public async Task<IActionResult> DeleteAllTags()
        {
            await _tagService.DeleteAllTags();
            return Ok();
        }
    }
}
