using Microsoft.Extensions.Configuration;
using RestfulBooker.ApiTests.Constants;
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
            request.AddHeader(HttpHeaders.Name.ContentType, HttpHeaders.Value.ApplicationJson);
        }

        public static void AddAuthorizationHeader(this RestRequest request)
        {

        }

        public static string GetAuthToken()
        {
            var client = new RestClient(RestfulBokerUrl + Endpoints.AuthorizationEndpoint);
            client.Authenticator = new HttpBasicAuthenticator(AuthorizationRequest.Username, AuthorizationRequest.Password);

            var request = new RestRequest("resource", Method.POST);
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
