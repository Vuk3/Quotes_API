using Quotes_API.Models;

namespace Quotes_API.Dtos
{
    public class QuoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int PositiveVotes { get; set; } = 0;
        public int NegativeVotes { get; set; } = 0;
        public List<Tag> Tags { get; set; } = new List<Tag>();

    }
}
