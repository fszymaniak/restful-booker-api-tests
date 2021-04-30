

namespace RestfulBooker.ApiTests.Configurations
{
    public static class ConfigurationOptions
    {
        private static readonly IConfigurationProvider AppsettingsProvider = new ConfigurationProvider();

        public static string RestfulBookerUrl => AppsettingsProvider.GetValue<string>("RestfulBookerUrl");
    }
}
