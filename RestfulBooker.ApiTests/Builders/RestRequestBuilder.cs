using RestfulBooker.ApiTests.Constants;
using RestSharp;

namespace RestfulBooker.ApiTests.Builders
{
    public class RestRequestBuilder : IRestRequestBuilder
    {
        private RestRequest _request;

        public RestRequestBuilder Create()
        {
            _request = new RestRequest();
            return this;
        }

        public RestRequestBuilder WithEndpoint(string endpoint)
        {
            _request.Resource = endpoint;
            return this;
        }

        public RestRequestBuilder WithMethod(Method method)
        {
            _request.Method = method;
            return this;
        }

        public RestRequestBuilder WithHeaders()
        {
            _request.AddHeader(HttpHeaders.Name.ContentType, HttpHeaders.Value.ApplicationJson);
            _request.AddHeader(HttpHeaders.Name.Accept, HttpHeaders.Value.ApplicationJson);
            return this;
        }

        public RestRequestBuilder WithAuthorizationHeaders()
        {
            var token = ApiTestBase.GetAuthToken();
            var headerValue = $"token={token}";
            _request.AddHeader(HttpHeaders.Name.Authorization, HttpHeaders.Value.AuthorizationBasic);
            _request.AddHeader(HttpHeaders.Name.Cookie, headerValue);
            return this;
        }

        public RestRequest Build() => _request;
    }
}
