using Microsoft.AspNetCore.Http.Connections;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using System.Text.Json;

namespace RestfulBooker.ApiTests
{
    public static class BookingTestBase
    {
        private static readonly RestClient Client = new RestClient(ApiTestBase.RestfulBokerUrl);

        public static BookingResponse CreateBooking(string firstName, string lastName, int totalPrice, bool depositPaid, string checkIn, string checkOut, string additionalNeeds)
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

            var response = Client.Execute<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingResponse>(response.Content);

            return result;
        }

        public static BookingModel GetBookingById(int bookingId)
        {
            var request = GetBookingByIdRequest(bookingId);

            var response = Client.Execute<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingModel>(response.Content);

            return result;
        }

        public static RestRequest GetBookingByIdRequest(int bookingId)
        {
            var request = new RestRequest(Endpoints.GetBookingByIdEndpoint, Method.GET);
            request.AddUrlSegment(Endpoints.GetBookingByIdSegment, bookingId);
            request.AddHeaders();

            return request;
        }

        public static RestRequest PostBookingRequest(BookingModel bookingRequest)
        {
            var jsonRequest = JsonSerializer.Serialize(bookingRequest);

            var request = new RestRequest(Endpoints.BookingEndpoint, Method.POST);
            request.AddHeaders();
            request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);

            return request;
        }
    }
}
