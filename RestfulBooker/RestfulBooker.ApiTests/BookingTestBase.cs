using System.Collections.Generic;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestfulBooker.ApiTests
{
    public abstract class BookingTestBase
    {
        private readonly RestClient _client = new RestClient(ApiTestBase.RestfulBokerUrl);

        public async Task<BookingResponse> CreateBooking(string firstName, string lastName, int totalPrice, bool depositPaid, string checkIn, string checkOut, string additionalNeeds)
        {
            var bookingDates = new BookingDates
            {
                CheckIn = checkIn,
                CheckOut = checkOut
            };

            var bookingRequest = new BookingModel
            {
                FirstName = firstName,
                LastName = lastName,
                TotalPrice = totalPrice,
                DepositPaid = depositPaid,
                BookinDates = bookingDates,
                AdditionalNeeds = additionalNeeds
            };

            var request = PostBookingRequest(bookingRequest);

            var response = await _client.ExecuteAsync<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingResponse>(response.Content);

            return result;
        }

        public async Task<BookingModel> GetBookingById(int bookingId)
        {
            var request = GetBookingByIdRequest(bookingId);

            var response = await _client.ExecuteAsync<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingModel>(response.Content);

            return result;
        }

        public async Task<IEnumerable<BookingResponse>> GetBookingIds()
        {
            var request = new RestRequest(Endpoints.BookingEndpoint, Method.GET);

            var response = await _client.ExecuteAsync<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<IEnumerable<BookingResponse>>(response.Content);

            return result;
        }

        public RestRequest GetBookingByIdRequest(int bookingId)
        {
            var request = new RestRequest(Endpoints.GetBookingByIdEndpoint, Method.GET);
            request.AddUrlSegment(Endpoints.GetBookingByIdSegment, bookingId);
            request.AddHeaders();

            return request;
        }

        public RestRequest PostBookingRequest(BookingModel bookingRequest)
        {
            var jsonRequest = JsonSerializer.Serialize(bookingRequest);

            var request = new RestRequest(Endpoints.BookingEndpoint, Method.POST);
            request.AddHeaders();
            request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);

            return request;
        }
    }
}
