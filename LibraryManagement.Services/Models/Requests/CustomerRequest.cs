using System.Text.Json.Serialization;

namespace LibraryManagement.Services.Models.Requests
{
    public class CustomerRequest
    {
        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }

    public class CustomerFilterRequest
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
    }
}
