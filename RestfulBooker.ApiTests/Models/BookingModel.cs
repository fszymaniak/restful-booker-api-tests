using System.Text.Json.Serialization;

namespace RestfulBooker.ApiTests.Models
{
    public class BookingModel
    {
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastname")]
        public string LastName { get; set; }

        [JsonPropertyName("totalprice")]
        public int TotalPrice { get; set; }

        [JsonPropertyName("depositpaid")]
        public bool DepositPaid { get; set; }

        [JsonPropertyName("bookingdates")]
        public BookingDates BookinDates { get; set; }

        [JsonPropertyName("additionalneeds")]
        public string AdditionalNeeds { get; set; }
    }
}
