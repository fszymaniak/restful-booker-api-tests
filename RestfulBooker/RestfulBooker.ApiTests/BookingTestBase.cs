using System.Collections.Generic;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;
using RestfulBooker.ApiTests.Models.Responses;

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
            var request = BookingByIdRequest(bookingId, Method.GET);

            var response = await _client.ExecuteAsync<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingModel>(response.Content);

            return result;
        }

        public async Task DeleteBookingById(int bookingId)
        {
            var request = BookingByIdRequest(bookingId, Method.DELETE);

            await _client.ExecuteAsync<BookingResponse>(request);
        }

        public async Task<IEnumerable<BookingResponse>> GetBookingIds()
        {
            var request = new RestRequest(Endpoints.BookingEndpoint, Method.GET);

            var response = await _client.ExecuteAsync<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<IEnumerable<BookingResponse>>(response.Content);

            return result;
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByFirstAndLastName(string firstName, string lastName)
        {
            var request = GetBookingByFirstAndLastNameRequest(firstName, lastName);

            var response = await _client.ExecuteAsync<BookingIdsResponse>(request);
            var result = JsonSerializer.Deserialize<IEnumerable<BookingIdsResponse>>(response.Content);

            return result;
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByCheckinAndCheckout(string checkin, string checkout)
        {
            var request = GetBookingByCheckinAndCheckoutRequest(checkin, checkout);

            var response = await _client.ExecuteAsync<BookingIdsResponse>(request);
            var result = JsonSerializer.Deserialize<IEnumerable<BookingIdsResponse>>(response.Content);

            return result;
        }

        public RestRequest BookingByIdRequest(int bookingId, Method method)
        {
            var request = new RestRequest(Endpoints.GetBookingByIdEndpoint, method);
            request.AddUrlSegment(Endpoints.GetBookingByIdSegment, bookingId);
            request.AddHeaders();
            request.AddAuthorizationHeader();

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

        public RestRequest GetBookingByFirstAndLastNameRequest(string firstName, string lastName)
        {
            var request = new RestRequest(Endpoints.BookingEndpoint, Method.GET);
            request.AddQueryParameter(Endpoints.GetBookingByFirstNameSegment, firstName);
            request.AddQueryParameter(Endpoints.GetBookingByLastNameSegment, lastName);
            request.AddHeaders();

            return request;
        }

        public RestRequest GetBookingByCheckinAndCheckoutRequest(string checkin, string checkout)
        {
            var request = new RestRequest(Endpoints.BookingEndpoint, Method.GET);
            request.AddQueryParameter(Endpoints.GetBookingByCheckinSegment, checkin);
            request.AddQueryParameter(Endpoints.GetBookingByCheckoutSegment, checkout);
            request.AddHeaders();

            return request;
        }
    }
}
