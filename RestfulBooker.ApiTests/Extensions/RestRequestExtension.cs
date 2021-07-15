using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Models;
using RestSharp;

namespace RestfulBooker.ApiTests.Extensions
{
    public static class RestRequestExtension
    {
        public static void SetUpRequestWithAdditionalInformation(this RestRequest request, Method method, string segment, int bookingId)
        {
            request.Method = method;
            request.AddUrlSegment(segment, bookingId);
        }

        public static void AddAuthorizationHeader(this RestRequest request)
        {
            var token = ApiTestBase.GetAuthToken();
            var headerValue = $"token={token}";
            request.AddHeader(HttpHeaders.Name.Authorization, HttpHeaders.Value.AuthorizationBasic);
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
            //var endpointWithMethod = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, method } };
            //request = request.Create(endpointWithMethod);
            request.SetUpRequestWithAdditionalInformation(method, Endpoints.GetBookingByIdSegment, bookingId);
            request.AddParameter(HttpHeaders.Value.ApplicationJson, jsonRequest, ParameterType.RequestBody);
            request.AddAuthorizationHeader();
        }

        public static void PostBookingRequest(this RestRequest request, BookingModel bookingRequest)
        {
            var jsonRequest = JsonSerializer.Serialize(bookingRequest);

            request.AddParameter(HttpHeaders.Value.ApplicationJson, jsonRequest, ParameterType.RequestBody);
        }

        public static void GetBookingByFirstAndLastNameRequest(this RestRequest request, string firstName, string lastName)
        {
            request.SetUpFilters(firstName, lastName, Endpoints.GetBookingByFirstNameSegment, Endpoints.GetBookingByLastNameSegment);
        }

        public static void GetBookingByCheckinAndCheckoutRequest(this RestRequest request, string checkin, string checkout)
        {
            request.SetUpFilters(checkin, checkout, Endpoints.GetBookingByCheckinSegment, Endpoints.GetBookingByCheckoutSegment);
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

        private static void SetUpFilters(this RestRequest request, string filter1, string filter2, string segment1, string segment2)
        {
            filter1.Trim();
            filter2.Trim();
            filter1 = filter1 == "<null>" ? null : filter1;
            filter2 = filter2 == "<null>" ? null : filter2;

            if (filter1 == null)
            {
                request.AddQueryParameter(segment2, filter2);
            }
            else if (filter2 == null)
            {
                request.AddQueryParameter(segment1, filter1);
            }
            else
            {
                request.AddQueryParameter(segment1, filter1);
                request.AddQueryParameter(segment2, filter2);
            }
        }
    }
}
