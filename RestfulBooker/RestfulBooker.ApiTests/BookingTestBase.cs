using Microsoft.AspNetCore.Http.Connections;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using System.Text.Json;

namespace RestfulBooker.ApiTests
{
    public static class BookingTestBase
    {
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

            var json = JsonSerializer.Serialize(bookingRequest);

            var client = new RestClient(ApiTestBase.RestfulBokerUrl);

            var request = new RestRequest(Endpoints.BookingEndpoint, Method.POST);
            request.AddHeaders();
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var response = client.Execute<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingResponse>(response.Content);

            return result;
        }

        public static BookingModel GetBookingById(int bookingId)
        {
            var client = new RestClient(ApiTestBase.RestfulBokerUrl);

            var request = new RestRequest(Endpoints.GetBookingByIdEndpoint, Method.GET);
            request.AddUrlSegment(Endpoints.GetBookingByIdSegment, bookingId);
            request.AddHeaders();

            var response = client.Execute<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingModel>(response.Content);

            return result;
        }
    }
}
