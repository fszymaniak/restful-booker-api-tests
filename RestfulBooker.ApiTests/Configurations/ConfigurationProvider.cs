using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace RestfulBooker.ApiTests.Configurations
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public T GetValue<T>(string name)
        {
            return Configuration.GetValue<T>(name);
        }

        private static IConfiguration Configuration => _lazyConfig.Value;

        private static readonly Lazy<IConfiguration> _lazyConfig = new Lazy<IConfiguration>(BuildConfiguration);

        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            return builder;
        }
    }
}
