using System;
using System.Text.Json.Serialization;

namespace RestfulBooker.ApiTests.Models
{
    public class BookingDates
    {
        [JsonPropertyName("checkin")]
        public DateTime CheckIn { get; set; }

        [JsonPropertyName("checkout")]
        public DateTime CheckOut { get; set; }
    }
}
