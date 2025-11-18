using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace RestfulBooker.ApiTests.Services
{
    public class ApiConfiguration
    {
        private readonly IConfiguration _configuration;

        public ApiConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Development";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public string RestfulBookerUrl =>
            Environment.GetEnvironmentVariable("RESTFUL_BOOKER_URL")
            ?? _configuration["RestfulBookerUrl"];

        public int DefaultTimeout =>
            int.TryParse(_configuration["DefaultTimeout"], out int timeout)
            ? timeout
            : 30000;

        public bool EnableRetry =>
            bool.TryParse(_configuration["EnableRetry"], out bool retry)
            ? retry
            : false;
    }
}
