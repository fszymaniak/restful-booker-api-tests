using System.Net;
using NUnit.Framework;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.TestData;
using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;
using Shouldly;

namespace RestfulBooker.ApiTests.Api
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture]
    public class PostBookingTests : BookingTestBase
    {
        [Test]
        public async Task PostBooking_CreatesValidBooking_WhenValidModelIsSent()
        {
            // given
            var bookingRequest = TestBookingModels.ValidBookingModel;

            // when
            var request = PostBookingRequest(bookingRequest);
            var postResponse = await _client.ExecuteAsync<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingResponse>(postResponse.Content);

            var getResult = await GetBookingById(result.BookingId);

            // then
            postResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            getResult.ShouldBeValid(bookingRequest);

            // clearing up
            await DeleteBookingById(result.BookingId);
        }

        [Test]
        public async Task PostBooking_CreatesValidBooking_WhenValidModeWithoutAdditionalInfoIsSent()
        {
            // given
            var bookingRequest = TestBookingModels.BookingModelWithoutAdditionalNeeds;

            // when
            var request = PostBookingRequest(bookingRequest);
            var postResponse = await _client.ExecuteAsync<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingResponse>(postResponse.Content);

            var getResult = await GetBookingById(result.BookingId);

            // then
            postResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            getResult.ShouldBeValid(bookingRequest);

            // clearing up
            await DeleteBookingById(result.BookingId);
        }

        [Test]
        public async Task PostBooking_CreatesValidBooking_WhenValidModeWithoutTotalPriceIsSent()
        {
            // given
            var bookingRequest = TestBookingModels.BookingModelWithoutTotalPrice;
            var finalBookingRequest = bookingRequest;
            finalBookingRequest.TotalPrice = 0;

            // when
            var request = PostBookingRequest(bookingRequest);
            var postResponse = await _client.ExecuteAsync<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingResponse>(postResponse.Content);

            var getResult = await GetBookingById(result.BookingId);

            // then
            postResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            getResult.ShouldBeValid(bookingRequest);

            // clearing up
            await DeleteBookingById(result.BookingId);
        }

        [Test]
        public async Task PostBooking_CreatesValidBooking_WhenValidModeWithoutDepositPaidIsSent()
        {
            // given
            var bookingRequest = TestBookingModels.BookingModelWithoutTotalPrice;
            var finalBookingRequest = bookingRequest;
            finalBookingRequest.DepositPaid = false;

            // when
            var request = PostBookingRequest(bookingRequest);
            var postResponse = await _client.ExecuteAsync<BookingResponse>(request);
            var result = JsonSerializer.Deserialize<BookingResponse>(postResponse.Content);

            var getResult = await GetBookingById(result.BookingId);

            // then
            postResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            getResult.ShouldBeValid(bookingRequest);

            // clearing up
            await DeleteBookingById(result.BookingId);
        }

        [Test, TestCaseSource(typeof(TestBookingModels), nameof(TestBookingModels.InvalidBookingModels))]
        public async Task PostBooking_Returns500InvalidServerError_WhenInvalidModelIsSent(BookingModel invalidBookingModel)
        {
            // given
            var bookingRequest = invalidBookingModel;

            // when
            var request = PostBookingRequest(bookingRequest);
            var response = await _client.ExecuteAsync<BookingResponse>(request);

            // then
            response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
        }
    }
}
