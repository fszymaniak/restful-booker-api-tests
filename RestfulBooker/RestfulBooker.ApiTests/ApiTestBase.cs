using Microsoft.Extensions.Configuration;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Models.RequestModel;
using RestfulBooker.ApiTests.Models.ResponseModel;
using RestSharp;
using RestSharp.Authenticators;
using Shouldly;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace RestfulBooker.ApiTests
{
    public static class ApiTestBase
    {
        public static string RestfulBokerUrl => Configuration["RestfulBokerUrl"];

        public static IConfiguration Configuration { get; set; }

        static ApiTestBase()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public static void AddHeaders(this RestRequest request)
        {
            request.AddHeader(HttpHeaders.Name.ContentType, HttpHeaders.Value.ApplicationJson);
            request.AddHeader(HttpHeaders.Name.Accept, HttpHeaders.Value.ApplicationJson);
        }

        public static void AddAuthorizationHeader(this RestRequest request)
        {
            var token = GetAuthToken();
            var headerValue = $"token={token}";
            request.AddHeader(HttpHeaders.Name.Cookie, headerValue);
        }

        public static string GetAuthToken()
        {
            var client = new RestClient(RestfulBokerUrl);
            
            var body = new AuthorizationRequest
            {
                Username = Authorization.Username,
                Password = Authorization.Password
            };

            var request = new RestRequest(Endpoints.AuthorizationEndpoint, Method.POST);
            var json = JsonSerializer.Serialize(body);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = client.Execute<AuthorizationResponse>(request);
            var result = JsonSerializer.Deserialize<AuthorizationResponse>(response.Content);
            if (result.Token != null)
            {
                return result.Token;
            }
            else
            {
                throw new Exception("Bad credentials");
            }
        }
    }
}
