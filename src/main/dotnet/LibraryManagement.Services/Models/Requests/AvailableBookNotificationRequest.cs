using System.Text.Json.Serialization;

namespace LibraryManagement.Services.Models.Requests
{
    public class AvailableBookNotificationRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("bookId")]
        public int? BookId { get; set; }

        [JsonPropertyName("lastBookEndDate")]
        public DateTime? LastBookEndDate { get; set; }

        [JsonPropertyName("isAvailable")]
        public bool? IsAvailable { get; set; }
    }
}
