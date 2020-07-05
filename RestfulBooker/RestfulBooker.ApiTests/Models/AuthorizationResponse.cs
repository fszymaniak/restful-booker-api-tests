using System.Text.Json.Serialization;

namespace RestfulBooker.ApiTests.Models.ResponseModel
{
    public class AuthorizationResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}
