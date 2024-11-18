using System.Collections.Generic;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using System.Threading.Tasks;
using RestfulBooker.ApiTests.Models.Responses;
using RestfulBooker.ApiTests.Services;
using RestfulBooker.ApiTests.Factories;

namespace RestfulBooker.ApiTests
{
    /// <summary>
    /// Base class for booking API tests providing common test helper methods.
    /// </summary>
    public abstract class BookingTestBase
    {
        protected readonly ApiConfiguration _config;
        protected readonly AuthenticationService _authService;
        protected readonly BookingApiClient _apiClient;
        protected readonly BookingRequestFactory _requestFactory;
        protected readonly ITestLogger _logger;

        protected BookingTestBase()
        {
            _config = new ApiConfiguration();
            _logger = new TestLogger(enableDebugLogging: false);
            _authService = new AuthenticationService(_config.RestfulBookerUrl);
            _apiClient = new BookingApiClient(_config.RestfulBookerUrl, _logger);
            _requestFactory = new BookingRequestFactory(_authService);
        }

        /// <summary>
        /// Creates a new booking with the specified details.
        /// </summary>
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

        /// <summary>
        /// Retrieves a booking by its ID.
        /// </summary>
        public async Task<BookingModel> GetBookingById(int bookingId)
        {
            var request = _requestFactory.CreateBookingByIdRequest(bookingId, Method.GET);
            return await _apiClient.GetBookingAsync(request);
        }

        /// <summary>
        /// Updates an existing booking using PUT or PATCH method.
        /// </summary>
        public async Task<BookingModel> UpdateBookingById(BookingModel bookingRequest, int bookingId, Method method)
        {
            var request = _requestFactory.CreateUpdateBookingRequest(bookingRequest, bookingId, method);
            return await _apiClient.UpdateBookingAsync(request);
        }

        /// <summary>
        /// Deletes a booking by its ID.
        /// </summary>
        public async Task DeleteBookingById(int bookingId)
        {
            var request = _requestFactory.CreateBookingByIdRequest(bookingId, Method.DELETE);
            await _apiClient.DeleteBookingAsync(request);
        }

        /// <summary>
        /// Retrieves all booking IDs.
        /// </summary>
        public async Task<IEnumerable<BookingResponse>> GetBookingIds()
        {
            var request = _requestFactory.CreateGetBookingIdsRequest();
            return await _apiClient.GetBookingIdsAsync(request);
        }

        /// <summary>
        /// Retrieves booking IDs filtered by first and last name.
        /// </summary>
        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByFirstAndLastName(string firstName, string lastName)
        {
            var request = _requestFactory.CreateGetBookingByNameRequest(firstName, lastName);
            return await _apiClient.GetFilteredBookingIdsAsync(request);
        }

        /// <summary>
        /// Retrieves booking IDs filtered by check-in and check-out dates.
        /// </summary>
        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByCheckinAndCheckout(string checkin, string checkout)
        {
            var request = _requestFactory.CreateGetBookingByDatesRequest(checkin, checkout);
            return await _apiClient.GetFilteredBookingIdsAsync(request);
        }

        /// <summary>
        /// Retrieves booking IDs filtered by a custom query parameter.
        /// </summary>
        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByQueryParameter(string parameterName, string parameterValue)
        {
            var request = _requestFactory.CreateGetBookingByQueryParameterRequest(parameterName, parameterValue);
            return await _apiClient.GetFilteredBookingIdsAsync(request);
        }
    }
}
