using System.Net;

namespace LibraryManagement.Services.Models.Response
{
    public class LibraryApiResponse
    {
        public object? data { get; set; }
        public string message { get; set; }
        public HttpStatusCode statusCode { get; set; }
    }

    public class BookAvailabilityResponse
    {
        public DateTime? BookAvailableDate { get; set; }
    }
}
