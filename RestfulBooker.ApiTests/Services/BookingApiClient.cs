using RestfulBooker.ApiTests.Helpers;
using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Models.Responses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RestfulBooker.ApiTests.Services
{
    /// <summary>
    /// HTTP client for Booking API operations with logging and error handling.
    /// </summary>
    public class BookingApiClient
    {
        private readonly RestClient _client;
        private readonly ITestLogger _logger;

        public BookingApiClient(string baseUrl, ITestLogger logger = null)
        {
            _client = new RestClient(baseUrl);
            _logger = logger ?? new TestLogger();
        }

        /// <summary>
        /// Creates a new booking via POST request.
        /// </summary>
        public async Task<BookingResponse> CreateBookingAsync(RestRequest request)
        {
            _logger.LogDebug("Creating new booking");
            var response = await ExecuteWithLoggingAsync<BookingResponse>(request, "CreateBooking").ConfigureAwait(false);
            return JsonHelper.Deserialize<BookingResponse>(response.Content);
        }

        /// <summary>
        /// Retrieves a booking by ID via GET request.
        /// </summary>
        public async Task<BookingModel> GetBookingAsync(RestRequest request)
        {
            _logger.LogDebug("Getting booking");
            var response = await ExecuteWithLoggingAsync<BookingModel>(request, "GetBooking").ConfigureAwait(false);
            return JsonHelper.Deserialize<BookingModel>(response.Content);
        }

        /// <summary>
        /// Updates an existing booking via PUT/PATCH request.
        /// </summary>
        public async Task<BookingModel> UpdateBookingAsync(RestRequest request)
        {
            _logger.LogDebug("Updating booking");
            var response = await ExecuteWithLoggingAsync<BookingModel>(request, "UpdateBooking").ConfigureAwait(false);
            return JsonHelper.Deserialize<BookingModel>(response.Content);
        }

        /// <summary>
        /// Deletes a booking via DELETE request.
        /// </summary>
        public async Task DeleteBookingAsync(RestRequest request)
        {
            _logger.LogDebug("Deleting booking");
            await ExecuteWithLoggingAsync(request, "DeleteBooking").ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves all booking IDs via GET request.
        /// </summary>
        public async Task<IEnumerable<BookingResponse>> GetBookingIdsAsync(RestRequest request)
        {
            _logger.LogDebug("Getting all booking IDs");
            var response = await ExecuteWithLoggingAsync<IEnumerable<BookingResponse>>(request, "GetBookingIds").ConfigureAwait(false);
            return JsonHelper.Deserialize<IEnumerable<BookingResponse>>(response.Content);
        }

        /// <summary>
        /// Retrieves filtered booking IDs via GET request with query parameters.
        /// </summary>
        public async Task<IEnumerable<BookingIdsResponse>> GetFilteredBookingIdsAsync(RestRequest request)
        {
            _logger.LogDebug("Getting filtered booking IDs");
            var response = await ExecuteWithLoggingAsync<IEnumerable<BookingIdsResponse>>(request, "GetFilteredBookingIds").ConfigureAwait(false);
            return JsonHelper.Deserialize<IEnumerable<BookingIdsResponse>>(response.Content);
        }

        private async Task<RestResponse<T>> ExecuteWithLoggingAsync<T>(RestRequest request, string operationName)
        {
            try
            {
                var response = await _client.ExecuteAsync<T>(request).ConfigureAwait(false);
                LogResponse(response, operationName);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{operationName} failed", ex);
                throw;
            }
        }

        private async Task<RestResponse> ExecuteWithLoggingAsync(RestRequest request, string operationName)
        {
            try
            {
                var response = await _client.ExecuteAsync(request).ConfigureAwait(false);
                LogResponse(response, operationName);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{operationName} failed", ex);
                throw;
            }
        }

        private void LogResponse(RestResponse response, string operationName)
        {
            if (response.IsSuccessful)
            {
                _logger.LogDebug($"{operationName} succeeded with status {response.StatusCode}");
            }
            else if (response.StatusCode >= HttpStatusCode.BadRequest && response.StatusCode < HttpStatusCode.InternalServerError)
            {
                _logger.LogWarning($"{operationName} returned client error {response.StatusCode}: {response.ErrorMessage}");
            }
            else if (response.StatusCode >= HttpStatusCode.InternalServerError)
            {
                _logger.LogError($"{operationName} returned server error {response.StatusCode}: {response.ErrorMessage}");
            }
        }
    }
}
