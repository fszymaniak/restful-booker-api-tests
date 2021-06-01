using NUnit.Framework;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Steps
{
    [Binding]
    public class CommonTestsSteps : BookingTestBase
    {

        private ScenarioContext _scenarioContext;

        private static readonly IDictionary<string, Method> _endpointWithMethod = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, Method.GET } };
        private readonly RestRequest _request = RestRequestExtension.Create(_endpointWithMethod);

        public CommonTestsSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [AfterScenario]
        private async Task CleanUp()
        {
            var bookingsIds = _scenarioContext.GetBookingsIds();

            await DeleteBookingsByIds(_request, bookingsIds);

            //DisposeSpecFlowContext();
        }

        [Given(@"bookings exist")]
        public async Task GivenBookingsExist(IList<BookingModel> bookingModels)
        {
            var bookings = new List<BookingResponse>();

            await foreach (var booking in CreateBookings(bookingModels))
            {
                bookings.Add(booking);
            }

            var bookingsIds = bookings
                .Select(b => b.BookingId)
                .ToList();

            _scenarioContext.SetExpectedBookings(bookingModels);

            _scenarioContext.SetBookingsIds(bookingsIds);
        }

        [When(@"GET Bookings Ids request is sent")]
        [Then(@"GET Bookings Ids request is sent")]
        public async Task WhenGETBookingsIdsRequestIsSent()
        {
            var bookingsIdsResponse = await GetBookingIds();

            _scenarioContext.SetBookingIdsResponses(bookingsIdsResponse);
        }

        [Then(@"expected bookings should exist")]
        public void ThenExpectedBookingsShouldExists()
        {
            var expectedBookingIds = _scenarioContext.GetBookingsIds();

            var expectedNumberOfBookingIds = expectedBookingIds.Count();

            var bookingsIdsResponse = _scenarioContext.GetBookingIdsResponses();

            var currentNumberOfBookingIds = bookingsIdsResponse.Count();

            var actualNumberOfBookingIds = currentNumberOfBookingIds - _scenarioContext.GetInitialNumberOfBookingIds();

            Assert.IsTrue(actualNumberOfBookingIds.Equals(expectedNumberOfBookingIds));
            bookingsIdsResponse.ShouldIncludesBookingIds(expectedBookingIds);
        }

        //[Then(@"bookings should not exist")]
        //public void ThenBookingsShouldNotExist()
        //{
        //    var expectedResults = ExpectedResults();

        //    Assert.IsFalse(expectedResults.ActualNumberOfBookingIds.Equals(expectedResults.ExpectedNumberOfBookingIds));
        //    expectedResults.BookingsIdsResponse.ShouldNotIncludesBookingIds(expectedResults.ExpectedBookingIds);
        //}


        [Then(@"expected bookings should return expected status code (.*)")]
        public void ThenExpectedBookingsShouldReturnExpectedStatusCode(HttpStatusCode statusCode)
        {
            var bookingModelResponses = _scenarioContext.GetRestBookingResponses();

            bookingModelResponses.ShouldHaveValidStatusCode(statusCode);
        }

        //private (int ActualNumberOfBookingIds, int ExpectedNumberOfBookingIds, IEnumerable<int> ExpectedBookingIds, IEnumerable<BookingIdsResponse> BookingsIdsResponse) ExpectedResults()
        //{
        //    var expectedBookingIds = _scenarioContext.GetBookingsIds();

        //    var expectedNumberOfBookingIds = expectedBookingIds.Count();

        //    var bookingsIdsResponse = _scenarioContext.GetBookingIdsResponses();

        //    var currentNumberOfBookingIds = bookingsIdsResponse.Count();

        //    var actualNumberOfBookingIds = currentNumberOfBookingIds - _scenarioContext.GetInitialNumberOfBookingIds();

        //    return (ActualNumberOfBookingIds: actualNumberOfBookingIds, ExpectedNumberOfBookingIds: expectedNumberOfBookingIds,
        //        ExpectedBookingIds: expectedBookingIds, BookingsIdsResponse: bookingsIdsResponse);
        //}

        private void DisposeSpecFlowContext()
        {
            try
            {
                var disposableContext = _scenarioContext as IDisposable;
                disposableContext.Dispose();
            }
            catch
            {
            }
        }
    }
}
