namespace Quotes_API.Models
{
    public class UserAndQuote
    {
        public string UserEmail { get; set; }
        public int QuoteId { get; set; }
        public int Reaction { get; set; }
    }
}
