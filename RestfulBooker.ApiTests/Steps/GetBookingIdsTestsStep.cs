using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using RestfulBooker.ApiTests.Extensions;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Steps
{
    [Parallelizable(ParallelScope.Fixtures)]
    [Binding]
    public class GetBookingIdsTestsStep : BookingTestBase
    {
        private int _expectedNumerOfBookings;
        private readonly ScenarioContext _scenarioContext;

        public GetBookingIdsTestsStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"GET filtered Bookings Ids by first and last name: (.*) (.*)")]
        public async Task WhenGETFilteredBookingsIdsByFirstAndLastName(string firstName, string lastName)
        {
            await SetFilteredQueries(firstName, lastName);

            var getExpectedBookings = _scenarioContext.GetExpectedBookings();

            _expectedNumerOfBookings =
                getExpectedBookings.Count(x => x.FirstName == firstName || x.LastName == lastName);
        }

        [When(@"GET filtered Bookings Ids by checkin and checkout: (.*) (.*)")]
        public async Task WhenGETFilteredBookingsIdsByCheckinAndCheckout(string checkin, string checkout)
        {
            await SetFilteredQueries(checkin, checkout);

            var getExpectedBookings = _scenarioContext.GetExpectedBookings();

            var format = "yyyy-MM-dd";
            var checkinDate = ParseDateTimeFromString(checkin, format);
            var checkoutDate = ParseDateTimeFromString(checkout, format);

            _expectedNumerOfBookings = getExpectedBookings.Count(x =>
                x.BookingDates.CheckIn >= checkinDate || x.BookingDates.CheckOut >= checkoutDate);
        }

        [Then(@"bookings Ids should be filtered properly")]
        public void ThenBookingsIdsShouldBeFilteredProperly()
        {
            var bookingsIdsResponse = _scenarioContext.GetBookingIdsResponses();

            var currentNumberOfBookingIds = bookingsIdsResponse.Count();

            var expectedBookingIds = _scenarioContext.GetBookingsIds();

            Assert.IsTrue(currentNumberOfBookingIds.Equals(_expectedNumerOfBookings));
            bookingsIdsResponse.ShouldIncludesBookingIds(expectedBookingIds);
        }

        private async Task SetFilteredQueries(string q1, string q2)
        {
            var results = await GetBookingIdsByQueryFilters(q1, q2);

            _scenarioContext.SetBookingIdsResponses(results);
        }

        private static DateTime ParseDateTimeFromString(string str, string format)
        {
            return DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);
        }
    }
}
