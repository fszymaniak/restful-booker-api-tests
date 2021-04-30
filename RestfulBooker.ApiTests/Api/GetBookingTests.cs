using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using Shouldly;

namespace RestfulBooker.ApiTests.Api
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture]
    public class GetBookingTests : BookingTestBase
    {
        private static IDictionary<string, Method> _endpointWithMethod = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, Method.GET } };
        private RestRequest _request = RestRequestExtension.Create(_endpointWithMethod);

        [Test]
        public async Task GetBooking_ReturnsValidBooking_WhenIdExists()
        {
            // given
            var booking = await CreateBooking("Phil", "Collins", 1000, true, "2020-08-23", "2020-08-30",
                "Breakfasts");

            // when
            var results =  await GetBookingById(booking.BookingId);

            // then
            results.ShouldBeValid(booking);

            // clearing up
            //await DeleteBookingsByIds(booking.BookingId);

        }

        [Test]
        public async Task GetBooking_ReturnsOk_WhenIdExists()
        {
            // given
            var booking = await CreateBooking("Jarred", "Jack", 2000, false, "2020-09-23", "2020-09-30",
                "Breakfasts");

            // when
            _request.BookingByIdRequest(booking.BookingId, Method.GET);
            var response = await _client.ExecuteAsync<BookingResponse>(_request);

            // then
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            // clearing up
            //await DeleteBookingsByIds(booking.BookingId);
        }

        [TestCase(0)]
        [TestCase(1000000)]
        [TestCase(-1)]
        public async Task GetBooking_ReturnsNotFound_WhenIdNotExists(int bookingId)
        {
            // given
            var notExistingBookingId = bookingId;

            // when
            _request.BookingByIdRequest(notExistingBookingId, Method.GET);
            var response = await _client.ExecuteAsync<BookingResponse>(_request);

            // then
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
