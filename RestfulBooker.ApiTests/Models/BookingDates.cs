using System;
using System.Text.Json.Serialization;
using RestSharp;

namespace RestfulBooker.ApiTests.Models
{
    public class BookingDates
    {
        [JsonPropertyName("checkin")]
        public string CheckIn { get; set; }

        [JsonPropertyName("checkout")]
        public string CheckOut { get; set; }

    }
}
