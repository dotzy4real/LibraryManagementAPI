using System.Text.Json.Serialization;

namespace LibraryManagement.Services.Models.Requests
{
    public class BookRequest
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("author")]
        public string? Author { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public class BookFilterRequest
    {

        [JsonPropertyName("bookTitle")]
        public required string BookTitle { get; set; }
    }

    public class ReturnBookRequest
    {
        [JsonPropertyName("bookId")]
        public required int? BookId { get; set; }
        [JsonPropertyName("customerId")]
        public required int? CustomerId { get; set; }
    }
}
