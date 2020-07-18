using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using Shouldly;

namespace RestfulBooker.ApiTests.Api
{
    public class GetBookingTests : BookingTestBase
    {
        private RestClient _client;

        [OneTimeSetUp]
        public void Init()
        {
            _client = new RestClient(ApiTestBase.RestfulBokerUrl);
        }

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
        }

        [Test]
        public async Task GetBooking_ReturnsOk_WhenIdExists()
        {
            // given
            var booking = await CreateBooking("Jarred", "Jack", 2000, false, "2020-09-23", "2020-09-30",
                "Breakfasts");

            // when
            var request = GetBookingByIdRequest(booking.BookingId);
            var response = await _client.ExecuteAsync<BookingResponse>(request);

            // then
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [TestCase(0)]
        [TestCase(1000000)]
        [TestCase(-1)]
        public async Task GetBooking_ReturnsNotFound_WhenIdNotExists(int bookingId)
        {
            // given
            var notExistingBookingId = bookingId;

            // when
            var request = GetBookingByIdRequest(notExistingBookingId);
            var response = await _client.ExecuteAsync<BookingResponse>(request);

            // then
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}