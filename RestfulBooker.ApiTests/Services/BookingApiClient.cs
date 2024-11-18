using RestfulBooker.ApiTests.Helpers;
using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Models.Responses;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestfulBooker.ApiTests.Services
{
    public class BookingApiClient
    {
        private readonly RestClient _client;

        public BookingApiClient(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public async Task<BookingResponse> CreateBookingAsync(RestRequest request)
        {
            var response = await _client.ExecuteAsync<BookingResponse>(request);
            return JsonHelper.Deserialize<BookingResponse>(response.Content);
        }

        public async Task<BookingModel> GetBookingAsync(RestRequest request)
        {
            var response = await _client.ExecuteAsync<BookingModel>(request);
            return JsonHelper.Deserialize<BookingModel>(response.Content);
        }

        public async Task<BookingModel> UpdateBookingAsync(RestRequest request)
        {
            var response = await _client.ExecuteAsync<BookingModel>(request);
            return JsonHelper.Deserialize<BookingModel>(response.Content);
        }

        public async Task DeleteBookingAsync(RestRequest request)
        {
            await _client.ExecuteAsync(request);
        }

        public async Task<IEnumerable<BookingResponse>> GetBookingIdsAsync(RestRequest request)
        {
            var response = await _client.ExecuteAsync<IEnumerable<BookingResponse>>(request);
            return JsonHelper.Deserialize<IEnumerable<BookingResponse>>(response.Content);
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetFilteredBookingIdsAsync(RestRequest request)
        {
            var response = await _client.ExecuteAsync<IEnumerable<BookingIdsResponse>>(request);
            return JsonHelper.Deserialize<IEnumerable<BookingIdsResponse>>(response.Content);
        }
    }
}
