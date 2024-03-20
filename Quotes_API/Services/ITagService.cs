using Microsoft.Extensions.Primitives;
using Quotes_API.Dtos;
using Quotes_API.Models;

namespace Quotes_API.Services
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllTags();
        Task<bool> AddTag(TagDto tag);
        Task<List<Tag>> GetTagsByString(string tagsString);
        Task<bool> DeleteAllTags();
    }
}