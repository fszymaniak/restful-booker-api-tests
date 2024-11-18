using System.Collections.Generic;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using System.Threading.Tasks;
using RestfulBooker.ApiTests.Models.Responses;
using RestfulBooker.ApiTests.Services;
using RestfulBooker.ApiTests.Factories;

namespace RestfulBooker.ApiTests
{
    public abstract class BookingTestBase
    {
        protected readonly ApiConfiguration _config;
        protected readonly AuthenticationService _authService;
        protected readonly BookingApiClient _apiClient;
        protected readonly BookingRequestFactory _requestFactory;

        protected BookingTestBase()
        {
            _config = new ApiConfiguration();
            _authService = new AuthenticationService(_config.RestfulBookerUrl);
            _apiClient = new BookingApiClient(_config.RestfulBookerUrl);
            _requestFactory = new BookingRequestFactory(_authService);
        }

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
                BookingDates = bookingDates,
                AdditionalNeeds = additionalNeeds
            };

            var request = _requestFactory.CreatePostBookingRequest(bookingRequest);
            return await _apiClient.CreateBookingAsync(request);
        }

        public async Task<BookingModel> GetBookingById(int bookingId)
        {
            var request = _requestFactory.CreateBookingByIdRequest(bookingId, Method.GET);
            return await _apiClient.GetBookingAsync(request);
        }

        public async Task<BookingModel> UpdateBookingById(BookingModel bookingRequest, int bookingId, Method method)
        {
            var request = _requestFactory.CreateUpdateBookingRequest(bookingRequest, bookingId, method);
            return await _apiClient.UpdateBookingAsync(request);
        }

        public async Task DeleteBookingById(int bookingId)
        {
            var request = _requestFactory.CreateBookingByIdRequest(bookingId, Method.DELETE);
            await _apiClient.DeleteBookingAsync(request);
        }

        public async Task<IEnumerable<BookingResponse>> GetBookingIds()
        {
            var request = _requestFactory.CreateGetBookingIdsRequest();
            return await _apiClient.GetBookingIdsAsync(request);
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByFirstAndLastName(string firstName, string lastName)
        {
            var request = _requestFactory.CreateGetBookingByNameRequest(firstName, lastName);
            return await _apiClient.GetFilteredBookingIdsAsync(request);
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByCheckinAndCheckout(string checkin, string checkout)
        {
            var request = _requestFactory.CreateGetBookingByDatesRequest(checkin, checkout);
            return await _apiClient.GetFilteredBookingIdsAsync(request);
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByQueryParameter(string parameterName, string parameterValue)
        {
            var request = _requestFactory.CreateGetBookingByQueryParameterRequest(parameterName, parameterValue);
            return await _apiClient.GetFilteredBookingIdsAsync(request);
        }
    }
}
