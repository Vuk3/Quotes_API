using System.ComponentModel.DataAnnotations;

namespace Quotes_API.Models
{
    public class LoginUser
    {
        [Key]
        public string UserName { get; set; }
        public ICollection<UserAndQuote> ReactedQuotes { get; set; } = new List<UserAndQuote>();
    }
}
