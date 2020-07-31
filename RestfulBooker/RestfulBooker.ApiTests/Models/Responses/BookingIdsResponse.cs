using System.Text.Json.Serialization;

namespace RestfulBooker.ApiTests.Models.Responses
{
    public class BookingIdsResponse
    {
        [JsonPropertyName("bookingid")]
        public int BookingId { get; set; }
    }
}
