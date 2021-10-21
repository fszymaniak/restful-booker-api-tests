using System.Collections.Generic;
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
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests
{
    [Binding]
    public class BookingTestBase
    {
        protected RestClient _client = RestClientExtension.CreateRestClient();

        private readonly RestRequest _requestPost = RestRequestExtension.Create(Endpoints.BookingEndpoint, Method.POST);

        private readonly RestRequest _requestGetBooking = RestRequestExtension.Create(Endpoints.BookingEndpoint, Method.GET);

        private readonly RestRequest _requestPutBooking = RestRequestExtension.Create(Endpoints.GetBookingByIdEndpoint, Method.PUT);

        private readonly RestRequest _requestPatchBooking = RestRequestExtension.Create(Endpoints.GetBookingByIdEndpoint, Method.PATCH);

        private readonly RestRequest _requestDeleteBooking = RestRequestExtension.Create(Endpoints.GetBookingByIdEndpoint, Method.DELETE);

        private RestRequest _request = new RestRequest();

        public async IAsyncEnumerable<BookingResponse> CreateBookings(IEnumerable<BookingModel> bookingModels)
        {
            for (int i = 0; i < bookingModels.Count(); i++)
            {
                _requestPost.RemoveBodyParameter(i, index: 2);
                _requestPost.PostBookingRequest(bookingModels.ToList()[i]);
                var response = await _client.ExecuteAsync<BookingResponse>(_requestPost);
                var result = JsonSerializer.Deserialize<BookingResponse>(response.Content);
                yield return result;
            }
        }

        public async Task<BookingModel> GetBookingById(int bookingId)
        {
            _request = RestRequestExtension.Create(Endpoints.GetBookingByIdEndpoint, Method.GET);
            _request.BookingByIdRequest(bookingId);

            var response = await _client.ExecuteAsync<BookingResponse>(_request);
            var result = JsonSerializer.Deserialize<BookingModel>(response.Content);

            return result;
        }

        public async Task<IList<BookingModel>> UpdateBookingById(IEnumerable<BookingModel> bookingModels, IEnumerable<int> bookingId, Method method)
        {
            _request = GetRequestForUpdateBooking(method);
            var bookingResponses = new List<IRestResponse<BookingResponse>>();
            var bookingModelResults = new List<BookingModel>();

            for (int i = 0; i < bookingModels.Count(); i++)
            {
                _request.RemoveBodyParameter(i, index: 2);
                _request.UpdateBookingByIdRequest(bookingModels.ToList()[i], bookingId.ToList()[i], method);
                var response = await _client.ExecuteAsync<BookingResponse>(_request);
                var result = JsonSerializer.Deserialize<BookingModel>(response.Content);
                bookingResponses.Add(response);
                bookingModelResults.Add(result);
            }

            return bookingModelResults;
        }

        public async Task<IList<IRestResponse<BookingResponse>>> UpdateBookingByIdResponse(IEnumerable<object> bookingModels, IEnumerable<int> bookingId, Method method)
        {
            _request = GetRequestForUpdateBooking(method);
            var bookingResponses = new List<IRestResponse<BookingResponse>>();

            for (int i = 0; i < bookingModels.Count(); i++)
            {
                _request.RemoveBodyParameter(i, index: 2);
                _request.UpdateBookingByIdRequest(bookingModels.ToList()[i], bookingId.ToList()[i], method);
                var response = await _client.ExecuteAsync<BookingResponse>(_request);
                bookingResponses.Add(response);
            }

            return bookingResponses;
        }

        public async Task DeleteBookingsByIds(RestRequest request, IEnumerable<int> bookingIds)
        {
            foreach (var id in bookingIds)
            {
                _requestDeleteBooking.BookingByIdRequest(id);

                await _client.ExecuteAsync<HttpResponse>(_requestDeleteBooking);

                _requestDeleteBooking.Parameters.RemoveAt(4);
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
            _requestGetBooking.GetBookingByQueryParameterRequest(parameterName, parameterValue);

            var response = await _client.ExecuteAsync<BookingIdsResponse>(_request);
            var result = JsonSerializer.Deserialize<IEnumerable<BookingIdsResponse>>(response.Content);

            return result;
        }

        private static RestRequest GetRequestForUpdateBooking(Method method)
        {
            return method switch
            {
                Method.PUT => RestRequestExtension.Create(Endpoints.GetBookingByIdEndpoint, Method.PUT),
                Method.PATCH => RestRequestExtension.Create(Endpoints.GetBookingByIdEndpoint, Method.PATCH),
                _ => throw new ArgumentOutOfRangeException(method.ToString())
            };
        }
    }
}
