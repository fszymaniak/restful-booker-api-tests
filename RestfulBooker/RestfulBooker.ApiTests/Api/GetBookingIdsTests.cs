using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using RestfulBooker.ApiTests.Models.Responses;
using Shouldly;

namespace RestfulBooker.ApiTests.Api
{
    public class GetBookingIdsTests : BookingTestBase
    {
        private int _expectedNumberOfFilteredBooking;
        private int _expectedNumberOfNotExistingBooking;

        [OneTimeSetUp]
        public void InitAsync()
        {
            _expectedNumberOfNotExistingBooking = 0;
            _expectedNumberOfFilteredBooking = 1;
        }
        
        [Test]
        public async Task GetBookingIds_ReturnOk_ForValidRequest()
        {
            // given
            var firstBooking = await CreateBooking("Jack", "Mamoa", 1000, true, "2020-08-23", "2020-08-30",
                "Breakfasts");

            var secondBooking = await CreateBooking("Kate", "Winslet", 1000, true, "2020-08-23", "2020-08-30",
                "Breakfasts");

            // when 
            var results = await GetBookingIds();

            // then
            results.Count().ShouldBeGreaterThanOrEqualTo(2);

            // clearing up
            await DeleteBookingById(firstBooking.BookingId);
            await DeleteBookingById(secondBooking.BookingId);
        }

        [Test]
        public async Task GetBookingIds_ReturnOk_ForValidRequestWithFirstAndLastName()
        {
            // given
            var createdBooking = await CreateBooking("Dirk", "Nowitzki", 1000, true, "2020-08-23", "2020-08-30",
                "Breakfasts");

            // when 
            var results = await GetBookingIdsByFirstAndLastName(createdBooking.Booking.FirstName, createdBooking.Booking.LastName);

            // then
            results.Count().ShouldBe(_expectedNumberOfFilteredBooking);
            results.First().BookingId.ShouldBe(createdBooking.BookingId);

            // clearing up
            await DeleteBookingById(createdBooking.BookingId);
        }

        [Test]
        public async Task GetBookingIds_ReturnsEmptyResponse_ForValidRequestWithFirstAndLastName()
        {
            // given
            var createdBooking = await CreateBooking("Dirk", "Nowitzki", 1000, true, "2020-08-23", "2020-08-30",
                "Breakfasts");

            var notExistingFirstName = "notExistingFirstName";
            var notExistingLastName = "notExistingLastName";

            // when 
            var results = await GetBookingIdsByFirstAndLastName(notExistingFirstName, notExistingLastName);

            // then
            results.Count().ShouldBe(_expectedNumberOfNotExistingBooking);

            // clearing up
            await DeleteBookingById(createdBooking.BookingId);
        }

        [TestCase("firstname", "Darell")]
        [TestCase("lastname", "Addams")]
        public async Task GetBookingIds_ReturnOk_ForValidRequestWithOneQueryParameter(string parameterName, string parameterValue)
        {
            // given
            var createdBooking = await CreateBooking("Darell", "Addams", 2000, true, "2019-09-23", "2019-09-30",
                "Breakfasts");

            // when 
            var results = await GetBookingIdsByQueryParameter(parameterName, parameterValue);

            // then
            results.Count().ShouldBe(_expectedNumberOfFilteredBooking);
            results.First().BookingId.ShouldBe(createdBooking.BookingId);

            // clearing up
            await DeleteBookingById(createdBooking.BookingId);
        }

        [TestCase("firstname", "notExistingFirstName")]
        [TestCase("lastname", "notExistingName")]
        public async Task GetBookingIds_ReturnsEmptyResponse_ForRequestWithNoExistingQueryParameter(string parameterName, string parameterValue)
        {
            // given
            var createdBooking = await CreateBooking("Darell", "Addams", 2000, true, "2019-09-23", "2019-09-30",
                "Breakfasts");

            // when 
            var results = await GetBookingIdsByQueryParameter(parameterName, parameterValue);

            // then
            results.Count().ShouldBe(_expectedNumberOfNotExistingBooking);

            // clearing up
            await DeleteBookingById(createdBooking.BookingId);
        }
    }
}
