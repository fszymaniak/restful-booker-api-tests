using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Models.RequestModel;
using RestfulBooker.ApiTests.Models.ResponseModel;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using RestfulBooker.ApiTests.Configurations;
using RestfulBooker.ApiTests.Extensions;

namespace RestfulBooker.ApiTests
{
    public static class ApiTestBase
    {
        private static readonly RestClient Client = new RestClient(ConfigurationOptions.RestfulBookerUrl);
        private static readonly RestRequest Request = RestRequestExtension.Create(Endpoints.AuthorizationEndpoint, Method.POST);


        public static string GetAuthToken()
        {
            Request.CreateRequestBody<AuthorizationRequest>(new AuthorizationRequest {
                Username = Authorization.Username,
                Password = Authorization.Password
            });

            var response = Client.Execute<AuthorizationResponse>(Request);

            return ValidateToken(response);
        }

        private static string ValidateToken(IRestResponse response)
        {
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
