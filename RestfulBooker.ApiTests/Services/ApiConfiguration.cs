using Microsoft.Extensions.Configuration;
using System.IO;

namespace RestfulBooker.ApiTests.Services
{
    public class ApiConfiguration
    {
        private readonly IConfiguration _configuration;

        public ApiConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public string RestfulBookerUrl => _configuration["RestfulBookerUrl"];
    }
}
