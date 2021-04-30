namespace RestfulBooker.ApiTests.Configurations
{
    public interface IConfigurationProvider
    {
        T GetValue<T>(string name);
    }
}