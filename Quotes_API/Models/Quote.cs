namespace Quotes_API.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int PositiveVotes { get; set; } = 0;
        public int NegativeVotes { get; set; } = 0;
        public ICollection<UserAndQuote> ReactionsByUsers{ get; set; } = new List<UserAndQuote>();
        public List<Tag> Tags { get; set; } = new List<Tag>();

    }
}
