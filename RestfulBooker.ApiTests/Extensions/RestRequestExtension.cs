using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using RestfulBooker.ApiTests.Builders;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Models;
using RestSharp;

namespace RestfulBooker.ApiTests.Extensions
{
    public static class RestRequestExtension
    {
        public static void SetUpRequestWithId(this RestRequest request, string segment, int bookingId)
        {
            request.AddUrlSegment(segment, bookingId);
        }

        public static RestRequest Create(string endpoint, Method method)
        {
            var builder = new RestRequestBuilder();

            var request = builder
                .Create()
                .WithEndpoint(endpoint)
                .WithMethod(method)
                .WithHeaders();

            if(NeedsAuthorization(method))
            {
                request.WithAuthorizationHeaders();
            }

            return request.Build();
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

        public static void BookingByIdRequest(this RestRequest request, int bookingId)
        {
            request.SetUpRequestWithId(Endpoints.GetBookingByIdSegment, bookingId);
        }

        public static void UpdateBookingByIdRequest(this RestRequest request, BookingModel bookingRequest, int bookingId, Method method)
        {
            var jsonRequest = JsonSerializer.Serialize(bookingRequest);
            //var endpointWithMethod = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, method } };
            //request = request.Create(endpointWithMethod);
            request.SetUpRequestWithId(Endpoints.GetBookingByIdSegment, bookingId);
            request.AddParameter(HttpHeaders.Value.ApplicationJson, jsonRequest, ParameterType.RequestBody);
            //request.AddAuthorizationHeader();
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
            request.AddQueryParameter(urlSegment, query);
        }

        public static void GetBookingByIdParameterRequest(this RestRequest request, int id)
        {
            request.AddUrlSegment(Endpoints.GetBookingByIdSegment, id.ToString());
        }

        public static string CreateBody<TBody>(object body)
        {
            return JsonSerializer.Serialize(body);
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

        private static bool NeedsAuthorization(Method method)
        {
            var methodsWithAutorization = new List<Method>() { Method.PUT, Method.PATCH, Method.DELETE };
            return methodsWithAutorization.Contains(method);
        }
    }
}
