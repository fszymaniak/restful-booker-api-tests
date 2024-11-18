using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Services;
using RestSharp;

namespace RestfulBooker.ApiTests.Helpers
{
    public static class RequestHelper
    {
        public static void AddStandardHeaders(this RestRequest request)
        {
            request.AddHeader(HttpHeaders.Name.ContentType, HttpHeaders.Value.ApplicationJson);
            request.AddHeader(HttpHeaders.Name.Accept, HttpHeaders.Value.ApplicationJson);
        }

        public static void AddAuthorizationHeader(this RestRequest request, AuthenticationService authService)
        {
            var token = authService.GetAuthToken();
            var headerValue = $"token={token}";
            request.AddHeader(HttpHeaders.Name.Cookie, headerValue);
        }

        public static void AddJsonBody<T>(this RestRequest request, T body)
        {
            var json = JsonHelper.Serialize(body);
            request.AddParameter(HttpHeaders.Value.ApplicationJson, json, ParameterType.RequestBody);
        }
    }
}
