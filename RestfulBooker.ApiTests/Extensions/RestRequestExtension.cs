using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Gherkin.Events.Args.Ast;
using RestfulBooker.ApiTests.Configurations;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Models;
using RestSharp;

namespace RestfulBooker.ApiTests.Extensions
{
    public static class RestRequestExtension
    {
        private static readonly IDictionary<string, Method> GetBookingEndpointDictionary = new Dictionary<string, Method>() { { Endpoints.BookingEndpoint, Method.GET } };


        public static void SetUpRequestWithAdditionalInformation(this RestRequest request, Method method, string segment, int bookingId)
        {
            request.AddUrlSegment(segment, bookingId);
            request.AddHeaders();
            request.AddAuthorizationHeader();
        }

        public static void AddQueryParameters(this RestRequest request, IDictionary<string, Method> endpointWithMethod, IEnumerable<IDictionary<string, string>> queryParameters)
        {
            //request = Create(endpointWithMethod);
            request = request.Create(endpointWithMethod);

            foreach (var parameter in queryParameters)
            {
                request.AddQueryParameter(parameter.FirstOrDefault().Key, parameter.FirstOrDefault().Value);
            }
        }

        public static void AddAuthorizationHeader(this RestRequest request)
        {
            var token = ApiTestBase.GetAuthToken();
            var headerValue = $"token={token}";
            request.AddHeader(HttpHeaders.Name.Cookie, headerValue);
        }

        public static RestRequest Create(this RestRequest request, IDictionary<string, Method> endpointWithMethod)
        {
            request = new RestRequest(endpointWithMethod.First().Key, endpointWithMethod.First().Value);
            request.AddHeaders();
            return request;

        }

        public static RestRequest Create(IDictionary<string, Method> endpointWithMethod)
        {
            var request = new RestRequest(endpointWithMethod.First().Key, endpointWithMethod.First().Value);
            request.AddHeaders();
            return request;
        }

        public static void RemoveBodyParameter(this RestRequest request, int iterator, int index)
        {
            if (iterator != 0)
            {
                request.Parameters.RemoveAt(index);
            }
        }

        public static void CreateRequestBody<TBody>(this RestRequest request, TBody body)
        {
            var json = CreateBody<TBody>(body);

            request.AddParameter(HttpHeaders.Value.ApplicationJson, json, ParameterType.RequestBody);
        }

        public static void BookingByIdRequest(this RestRequest request, int bookingId, Method method)
        {
            
            request.SetUpRequestWithAdditionalInformation(method, Endpoints.GetBookingByIdSegment, bookingId);
        }

        public static void UpdateBookingByIdRequest(this RestRequest request, BookingModel bookingRequest, int bookingId, Method method)
        {
            var jsonRequest = JsonSerializer.Serialize(bookingRequest);
            var endpointWithMethod = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, method } };
            request = request.Create(endpointWithMethod);
            request.SetUpRequestWithAdditionalInformation(method, Endpoints.GetBookingByIdSegment, bookingId);
            request.AddParameter(HttpHeaders.Value.ApplicationJson, jsonRequest, ParameterType.RequestBody);
        }

        public static void PostBookingRequest(this RestRequest request, BookingModel bookingRequest)
        {
            var jsonRequest = JsonSerializer.Serialize(bookingRequest);

            request.AddParameter(HttpHeaders.Value.ApplicationJson, jsonRequest, ParameterType.RequestBody);
        }

        public static void GetBookingByFirstAndLastNameRequest(this RestRequest request, IEnumerable<IDictionary<string, string>> fullNamesDictionary)
        {
            request.AddQueryParameters(GetBookingEndpointDictionary, fullNamesDictionary);
        }

        public static void GetBookingByCheckinAndCheckoutRequest(this RestRequest request, IEnumerable<IDictionary<string, string>> checkInCheckOutDictionary)
        {
            request.AddQueryParameters(GetBookingEndpointDictionary, checkInCheckOutDictionary);
        }

        public static void GetBookingByQueryParameterRequest(this RestRequest request, string urlSegment, string query)
        {
            request = new RestRequest(Endpoints.BookingEndpoint, Method.GET);
            request.AddQueryParameter(urlSegment, query);
        }

        public static string CreateBody<TBody>(object body)
        {
            return JsonSerializer.Serialize(body);
        }

        private static void AddHeaders(this RestRequest request)
        {
            request.AddHeader(HttpHeaders.Name.ContentType, HttpHeaders.Value.ApplicationJson);
            request.AddHeader(HttpHeaders.Name.Accept, HttpHeaders.Value.ApplicationJson);
        }
    }
}
