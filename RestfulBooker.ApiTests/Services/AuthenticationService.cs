using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Helpers;
using RestfulBooker.ApiTests.Models.RequestModel;
using RestfulBooker.ApiTests.Models.ResponseModel;
using RestSharp;
using System;

namespace RestfulBooker.ApiTests.Services
{
    public class AuthenticationService
    {
        private readonly string _baseUrl;
        private string _cachedToken;

        public AuthenticationService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public string GetAuthToken()
        {
            if (!string.IsNullOrEmpty(_cachedToken))
            {
                return _cachedToken;
            }

            var client = new RestClient(_baseUrl);

            var body = new AuthorizationRequest
            {
                Username = Authorization.Username,
                Password = Authorization.Password
            };

            var request = new RestRequest(Endpoints.AuthorizationEndpoint, Method.POST);
            var json = JsonHelper.Serialize(body);
            request.AddParameter(HttpHeaders.Value.ApplicationJson, json, ParameterType.RequestBody);

            var response = client.Execute<AuthorizationResponse>(request);
            var result = JsonHelper.Deserialize<AuthorizationResponse>(response.Content);

            if (result?.Token != null)
            {
                _cachedToken = result.Token;
                return _cachedToken;
            }
            else
            {
                throw new Exception("Bad credentials");
            }
        }
    }
}
