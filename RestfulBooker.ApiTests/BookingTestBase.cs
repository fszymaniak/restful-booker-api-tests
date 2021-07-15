﻿using System.Collections.Generic;
using System.Linq;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models.Responses;
using System.Text.RegularExpressions;
using System;
using System.Globalization;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests
{
    public abstract class BookingTestBase
    {
        //    private static readonly IDictionary<string, Method> PostBookingEndpointDictionary = new Dictionary<string, Method>() { { Endpoints.BookingEndpoint, Method.POST } };
        //    private static readonly IDictionary<string, Method> GetBookingEndpointDictionary = new Dictionary<string, Method>() { { Endpoints.BookingEndpoint, Method.GET } };
        //    private static readonly IDictionary<string, Method> PutBookingEndpointDictionary = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, Method.PUT } };
        //    private static readonly IDictionary<string, Method> PatchBookingEndpointDictionary = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, Method.PATCH } };
        //    private readonly IDictionary<string, Method> _getBookingByIdEndpointDictionary = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, Method.GET } };

        private static readonly IDictionary<string, Method> PostBookingEndpointDictionary = GetEndpointDictionary(Endpoints.BookingEndpoint, Method.POST);
        private static readonly IDictionary<string, Method> GetBookingEndpointDictionary = GetEndpointDictionary(Endpoints.BookingEndpoint, Method.GET);
        private static readonly IDictionary<string, Method> PutBookingEndpointDictionary = GetEndpointDictionary(Endpoints.GetBookingByIdEndpoint, Method.PUT);
        private static readonly IDictionary<string, Method> PatchBookingEndpointDictionary = GetEndpointDictionary(Endpoints.GetBookingByIdEndpoint, Method.PATCH);
        private readonly IDictionary<string, Method> _getBookingByIdEndpointDictionary = GetEndpointDictionary(Endpoints.GetBookingByIdEndpoint, Method.GET);

        protected RestClient _client = RestClientExtension.CreateRestClient();

        private readonly RestRequest _requestPost = CreateRequest(PostBookingEndpointDictionary);

        private readonly RestRequest _requestGetBooking = CreateRequest(GetBookingEndpointDictionary);

        //private readonly RestRequest _requestPutBooking = RestRequestExtension.Create(PutBookingEndpointDictionary);

        //private readonly RestRequest _requestPatchBooking = RestRequestExtension.Create(PatchBookingEndpointDictionary);

        private RestRequest _request = new RestRequest();

        public async IAsyncEnumerable<BookingResponse> CreateBookings(IEnumerable<BookingModel> bookingModels)
        {
            //IList<BookingResponse> bookingResponses = new List<BookingResponse>();

            for (int i = 0; i < bookingModels.Count(); i++)
            {
                _requestPost.RemoveBodyParameter(i, index: 2);
                _requestPost.PostBookingRequest(bookingModels.ToList()[i]);
                var response = await _client.ExecuteAsync<BookingResponse>(_requestPost);
                var result = JsonSerializer.Deserialize<BookingResponse>(response.Content);
                yield return result;
            }

            //return bookingResponses;
        }

        public async Task<BookingModel> GetBookingById(int bookingId)
        {
            _request = RestRequestExtension.Create(_getBookingByIdEndpointDictionary);
            _request.BookingByIdRequest(bookingId, Method.GET);

            var response = await _client.ExecuteAsync<BookingResponse>(_request);
            var result = JsonSerializer.Deserialize<BookingModel>(response.Content);

            return result;
        }

        public async IAsyncEnumerable<BookingResponse> UpdateBookingById(IEnumerable<BookingModel> bookingModels, IEnumerable<int> bookingId, Method method)
        {
            _request = GetRequestForUpdateBooking(method);

            for (int i = 0; i < bookingModels.Count(); i++)
            {
                _request.RemoveBodyParameter(i, index: 2);
                _request.UpdateBookingByIdRequest(bookingModels.ToList()[i], bookingId.ToList()[i], method);
                var response = await _client.ExecuteAsync<BookingResponse>(_requestPost);
                var result = JsonSerializer.Deserialize<BookingResponse>(response.Content);
                yield return result;
            }
        }

        public async Task DeleteBookingsByIds(RestRequest request, IEnumerable<int> bookingIds)
        {
            request.AddAuthorizationHeader();

            foreach (var id in bookingIds)
            {
                request.BookingByIdRequest(id, Method.DELETE);

                await _client.ExecuteAsync<HttpResponse>(request);

                request.Parameters.RemoveAt(4);
            }
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIds()
        {
            return await _client.RestResponseAsync<BookingIdsResponse, BookingIdsResponse>(_requestGetBooking);
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByQueryFilters(string filter1, string filter2)
        {
            string pattern = @"^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])$";
            var regex = new Regex(pattern);
            if ((regex.IsMatch(filter1) && regex.IsMatch(filter2)) || (regex.IsMatch(filter1) ^ regex.IsMatch(filter2)))
            {
                _requestGetBooking.GetBookingByCheckinAndCheckoutRequest(filter1, filter2);
            }
            else
            {
                _requestGetBooking.GetBookingByFirstAndLastNameRequest(filter1, filter2);

            }
            var response = await _client.ExecuteAsync<BookingIdsResponse>(_requestGetBooking);
            var result = JsonSerializer.Deserialize<IEnumerable<BookingIdsResponse>>(response.Content);

            return result;
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByCheckinAndCheckout(string checkin, string checkout)
        {
            //_request.GetBookingByCheckinAndCheckoutRequest(checkin, checkout);

            var response = await _client.ExecuteAsync<BookingIdsResponse>(_request);
            var result = JsonSerializer.Deserialize<IEnumerable<BookingIdsResponse>>(response.Content);

            return result;
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByQueryParameter(string parameterName, string parameterValue)
        {
            _request.GetBookingByQueryParameterRequest(parameterName, parameterValue);

            var response = await _client.ExecuteAsync<BookingIdsResponse>(_request);
            var result = JsonSerializer.Deserialize<IEnumerable<BookingIdsResponse>>(response.Content);

            return result;
        }

        public IEnumerable<BookingModel> TransformToBookingModelWithoutExcludedRow(string excludedRow, Table table)
        {
            return excludedRow switch
            {
                "FirstName" => table.Rows
                                       .Select(r => new BookingModel
                                       {
                                           LastName = r["LastName"],
                                           TotalPrice = int.Parse(r["TotalPrice"]),
                                           DepositPaid = bool.Parse(r["DepositPaid"]),
                                           BookingDates = GetBookingDates(r["BookingDates"]),
                                           AdditionalNeeds = r["AdditionalNeeds"]
                                       }).ToList(),
                "LastName" => table.Rows
                                    .Select(r => new BookingModel
                                    {
                                        FirstName = r["FirstName"],
                                        TotalPrice = int.Parse(r["TotalPrice"]),
                                        DepositPaid = bool.Parse(r["DepositPaid"]),
                                        BookingDates = GetBookingDates(r["BookingDates"]),
                                        AdditionalNeeds = r["AdditionalNeeds"]
                                    }).ToList(),
                "BookingDates" => table.Rows
                                    .Select(r => new BookingModel
                                    {
                                        FirstName = r["FirstName"],
                                        LastName = r["LastName"],
                                        TotalPrice = int.Parse(r["TotalPrice"]),
                                        DepositPaid = bool.Parse(r["DepositPaid"]),
                                        AdditionalNeeds = r["AdditionalNeeds"]
                                    }).ToList(),
                "AdditionalNeeds" => table.Rows
                                       .Select(r => new BookingModel
                                       {
                                           FirstName = r["FirstName"],
                                           LastName = r["LastName"],
                                           TotalPrice = int.Parse(r["TotalPrice"]),
                                           DepositPaid = bool.Parse(r["DepositPaid"]),
                                           BookingDates = GetBookingDates(r["BookingDates"]),
                                       }).ToList(),
                "TotalPrice" => table.Rows
                                    .Select(r => new BookingModel
                                    {
                                        FirstName = r["FirstName"],
                                        LastName = r["LastName"],
                                        DepositPaid = bool.Parse(r["DepositPaid"]),
                                        BookingDates = GetBookingDates(r["BookingDates"]),
                                        AdditionalNeeds = r["AdditionalNeeds"]
                                    }).ToList(),
                "DepositPaid" => table.Rows
                                    .Select(r => new BookingModel
                                    {
                                        FirstName = r["FirstName"],
                                        LastName = r["LastName"],
                                        TotalPrice = int.Parse(r["TotalPrice"]),
                                        BookingDates = GetBookingDates(r["BookingDates"]),
                                        AdditionalNeeds = r["AdditionalNeeds"]
                                    }).ToList(),
                _ => throw new ArgumentOutOfRangeException(excludedRow)
            };
        }

        protected static BookingDates GetBookingDates(string bookingDates, string part = null)
        {
            var parts = bookingDates.Split('/').Select(p => p.Trim()).ToList();
            var format = "yyyy-MM-dd";

            return new BookingDates()
            {
                CheckIn = ParseDateTimeFromString(parts.First(), format),
                CheckOut = ParseDateTimeFromString(parts.Last(), format)
            };
        }

        private static DateTime ParseDateTimeFromString(string str, string format)
        {
            return DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);
        }

        private static RestRequest GetRequestForUpdateBooking(Method method)
        {
            return method switch
            {
                Method.PUT => RestRequestExtension.Create(PutBookingEndpointDictionary),
                Method.PATCH => RestRequestExtension.Create(PatchBookingEndpointDictionary),
                _ => throw new ArgumentOutOfRangeException(method.ToString())
            };
        }

        private static Dictionary<string, Method> GetEndpointDictionary(string endpoint, Method method)
        {
            return new Dictionary<string, Method>() { { endpoint, method } };
        }

        private static RestRequest CreateRequest(IDictionary<string, Method> dictionary)
        {
            return RestRequestExtension.Create(dictionary);
        }
    }
}
