using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using RestfulBooker.ApiTests.Configurations;
using RestfulBooker.ApiTests.Models;
using RestSharp;

namespace RestfulBooker.ApiTests.Extensions
{
    public static class RestClientExtension
    {
        public static RestClient CreateRestClient()
        {
            return new RestClient(ConfigurationOptions.RestfulBookerUrl);
        }

        public static async Task<IEnumerable<TModel>> RestResponseAsync<TResponse, TModel>(this RestClient client, RestRequest request)
        {
            var response = await client.ExecuteAsync<TResponse>(request);
            return JsonSerializer.Deserialize<IEnumerable<TModel>>(response.Content);
        }
    }
}
