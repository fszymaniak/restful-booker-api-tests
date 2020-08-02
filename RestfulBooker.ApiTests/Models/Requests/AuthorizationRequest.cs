using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestfulBooker.ApiTests.Models.RequestModel
{
    public class AuthorizationRequest
    {
        [Required]
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
