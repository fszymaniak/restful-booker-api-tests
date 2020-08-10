using System;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.TestData;
using RestSharp;
using Shouldly;

namespace RestfulBooker.ApiTests.Api
{
    public class UpdateBookingTests : BookingTestBase
    {
        [Test]
        public async Task PutBooking_UpdateAllFieldsOfBooking_WhenValidModelIsSent()
        {
            // given
            var booking = await CreateBooking("Mary", "Jane", 1000, true, "2020-08-23", "2020-08-30",
                "Breakfasts");
            var bookingRequest = TestBookingModels.ValidBookingModel;

            // when
            var results = await UpdateBookingById(bookingRequest, booking.BookingId, Method.PUT);

            // then
            results.ShouldBeValid(bookingRequest);

            // clearing up
            await DeleteBookingById(booking.BookingId);
        }

        [Test]
        public async Task PutBooking_ReturnsOK_WhenBookingIsUpdated()
        {
            // given
            var booking = await CreateBooking("Mary", "Jane", 1000, true, "2020-08-23", "2020-08-30",
                "Breakfasts");
            var bookingRequest = TestBookingModels.ValidBookingModel;

            // when
            var request = UpdateBookingByIdRequest(bookingRequest, booking.BookingId, Method.PUT);
            var response = await _client.ExecuteAsync<BookingResponse>(request);

            // then
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            // clearing up
            await DeleteBookingById(booking.BookingId);
        }

        [Test]
        public async Task PutBooking_UpdateFewFieldsOfBooking_WhenValidModelIsSent()
        {
            // given
            var booking = await CreateBooking("John", "Wick", 2000, false, DateTime.Now.AddDays(5).ToString("yyyy/MM/dd"), DateTime.Now.AddDays(12).ToString("yyyy/MM/dd"),
                "Dinner");
            var bookingRequest = TestBookingModels.ValidBookingModel;

            // when
            var results = await UpdateBookingById(bookingRequest, booking.BookingId, Method.PUT);

            // then
            results.ShouldBeValid(bookingRequest);

            // clearing up
            await DeleteBookingById(booking.BookingId);
        }

        [Test]
        public async Task PutBooking_ReturnsOK_WhenBookingWithFewFieldsIsUpdated()
        {
            // given
            var booking = await CreateBooking("John", "Wick", 2000, false, DateTime.Now.AddDays(5).ToString("yyyy/MM/dd"), DateTime.Now.AddDays(12).ToString("yyyy/MM/dd"),
                "Dinner");
            var bookingRequest = TestBookingModels.ValidBookingModel;

            // when
            var request = UpdateBookingByIdRequest(bookingRequest, booking.BookingId, Method.PUT);
            var response = await _client.ExecuteAsync<BookingResponse>(request);

            // then
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            // clearing up
            await DeleteBookingById(booking.BookingId);
        }

        [Test, TestCaseSource(typeof(TestBookingModels), nameof(TestBookingModels.InvalidBookingModels))]
        public async Task PostBooking_Returns400BadRequest_WhenInvalidModelIsSent(BookingModel invalidBookingModel)
        {
            // given
            var booking = await CreateBooking("John", "Wick", 2000, false, DateTime.Now.AddDays(5).ToString("yyyy/MM/dd"), DateTime.Now.AddDays(12).ToString("yyyy/MM/dd"),
                "Dinner");
            var bookingRequest = invalidBookingModel;

            // when
            var request = UpdateBookingByIdRequest(bookingRequest, booking.BookingId, Method.PUT);
            var response = await _client.ExecuteAsync<BookingResponse>(request);

            // then
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            // clearing up
            await DeleteBookingById(booking.BookingId);
        }

        [Test]
        public async Task PutBooking_Returns405MethodNotAllowed_WhenTryToCreateBooking()
        {
            // given
            var bookingRequest = TestBookingModels.ValidBookingModel;
            var bookingId = 100;

            // when
            var request = UpdateBookingByIdRequest(bookingRequest, bookingId, Method.PUT);
            var response = await _client.ExecuteAsync<BookingResponse>(request);

            // then
            response.StatusCode.ShouldBe(HttpStatusCode.MethodNotAllowed);

            // clearing up
            await DeleteBookingById(bookingId);
        }
    }
}
