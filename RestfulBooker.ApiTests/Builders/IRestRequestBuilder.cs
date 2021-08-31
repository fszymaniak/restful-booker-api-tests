using RestSharp;

namespace RestfulBooker.ApiTests.Builders
{
    public interface IRestRequestBuilder
    {
        RestRequestBuilder Create();

        RestRequest Build();

        RestRequestBuilder WithEndpoint(string endpoint);

        RestRequestBuilder WithMethod(Method method);

        RestRequestBuilder WithHeaders();

        RestRequestBuilder WithAuthorizationHeaders();
    }
}
