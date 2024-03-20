using Microsoft.EntityFrameworkCore;
using Quotes_API.Contexts;
using Quotes_API.Dtos;
using Quotes_API.Migrations;
using Quotes_API.Models;

namespace Quotes_API.Services
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;
        public TagService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<bool> AddTag(TagDto tagDto)
        {
            var existingTag = _context.Tags.FirstOrDefault(t => t.Value == tagDto.Value);
            if (existingTag == null)
            {
                var tag = new Tag
                {
                    Value = tagDto.Value
                };
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAllTags()
        {
            try
            {
                foreach (var quote in _context.Quotes)
                {
                    quote.Tags.Clear();
                }

                _context.Tags.RemoveRange(_context.Tags);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await _context.Tags.ToListAsync();

        }

        public async Task<List<Tag>> GetTagsByString(string tagsString)
        {
            if(tagsString != null)
            {
                var tagList = tagsString.ToString().Split(',');
                var tagEntities = await _context.Tags
                    .Where(t => tagList.Contains(t.Value))
                    .ToListAsync();
                return tagEntities;
            }
            return null;

        }
    }
}
