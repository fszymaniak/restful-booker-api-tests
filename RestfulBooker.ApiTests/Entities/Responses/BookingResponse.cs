using System.Text.Json.Serialization;

namespace RestfulBooker.ApiTests.Models
{
    public class BookingResponse
    {
        [JsonPropertyName("bookingid")]
        public int BookingId { get; set; }

        [JsonPropertyName("booking")]
        public BookingModel Booking { get; set; }
    }
}
