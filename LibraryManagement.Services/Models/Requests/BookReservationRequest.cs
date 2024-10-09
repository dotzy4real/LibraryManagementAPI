using System.Text.Json.Serialization;

namespace LibraryManagement.Services.Models.Requests
{
    public class BookReservationRequest
    {
        [JsonPropertyName("bookId")]
        public int? BookId { get; set; }

        [JsonPropertyName("customerId")]
        public int? CustomerId { get; set; }

        [JsonPropertyName("requestAvailabilityNotification")]
        public bool RequestAvailabilityNotification { get; set; }
    }

    public class BorrowBookRequest
    {
        [JsonPropertyName("startDate")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }
    }
}
