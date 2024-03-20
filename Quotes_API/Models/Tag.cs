using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Quotes_API.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Value { get; set; }
        [JsonIgnore]
        public List<Quote> Quotes { get; set; } = new List<Quote>();
    }
}
