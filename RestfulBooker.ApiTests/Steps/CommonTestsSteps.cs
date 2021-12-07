using NUnit.Framework;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Models.Responses;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Steps
{
    [Binding]
    public class CommonTestsSteps : BookingTestBase
    {
        private ScenarioContext _scenarioContext;

        //private static readonly IDictionary<string, Method> _endpointWithMethod = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, Method.GET } };
        private readonly RestRequest _request = RestRequestExtension.Create(Endpoints.GetBookingByIdEndpoint, Method.GET);

        public CommonTestsSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
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

        [StepDefinition(@"GET Booking by Id request is sent")]
        public async Task WhenGetBookingByIdRequestIsSent()
        {
            var bookingsIds = _scenarioContext.GetBookingsIds();

            var bookingModels = new List<BookingModel>();

            var receivedBooking = await GetBookingsById(bookingsIds.ToArray());

            foreach (var model in receivedBooking)
            {
                bookingModels.Add(model);
            }

            _scenarioContext.SetBookingModels(bookingModels);
        }

        [StepDefinition(@"GET Bookings Ids request is sent")]
        public async Task WhenGETBookingsIdsRequestIsSent()
        {
            var bookingsIdsResponse = await GetBookingIds();

            _scenarioContext.SetBookingIdsResponses(bookingsIdsResponse);
        }

        [Then(@"bookings should not be updated")]
        public void ThenBookingsShouldNotBeUpdated()
        {
            var expectedBookingModels = _scenarioContext.GetExpectedBookings();
            var actualBookingModels = _scenarioContext.GetBookingModels();

            actualBookingModels.ShouldBeValid(expectedBookingModels);
        }

        [Then(@"objects should not be updated")]
        public void ThenObjectsShouldNotBeUpdated()
        {
            var expectedBookingModels = _scenarioContext.GetExpectedObjects();
            var actualBookingModels = _scenarioContext.GetExpectedObjects();

            actualBookingModels.ShouldBeValid(expectedBookingModels);
        }


        [Then(@"expected bookings should exist")]
        public void ThenExpectedBookingsShouldExists()
        {
            var expectedResults = ExpectedResults();

            Assert.IsTrue(expectedResults.ActualNumberOfBookingIds.Equals(expectedResults.ExpectedNumberOfBookingIds));
            expectedResults.BookingsIdsResponse.ShouldIncludesBookingIds(expectedResults.ExpectedBookingIds);
        }

        [Then(@"bookings should not exist")]
        public void ThenBookingsShouldNotExist()
        {
            var numberOfookingsIds = _scenarioContext.GetInitialNumberOfBookingIds();

            var currentNumberOfBookingIds = GetBookingIds().Result.Count();

            Assert.IsTrue(numberOfookingsIds.Equals(currentNumberOfBookingIds));
        }   

        [Then(@"actual bookings should return expected status code (.*)")]
        public void ThenActualBookingsShouldReturnExpectedStatusCode(HttpStatusCode statusCode)
        {
            var bookingModelResponses = _scenarioContext.GetRestBookingResponses();

            bookingModelResponses.ShouldHaveValidStatusCode(statusCode);
        }

        private (int ActualNumberOfBookingIds, int ExpectedNumberOfBookingIds, IEnumerable<int> ExpectedBookingIds, IEnumerable<BookingIdsResponse> BookingsIdsResponse) ExpectedResults()
        {
            var expectedBookingIds = _scenarioContext.GetBookingsIds();

            var expectedNumberOfBookingIds = expectedBookingIds.Count();

            var bookingsIdsResponse = _scenarioContext.GetBookingIdsResponses();

            var currentNumberOfBookingIds = bookingsIdsResponse.Count();

            var actualNumberOfBookingIds = currentNumberOfBookingIds - _scenarioContext.GetInitialNumberOfBookingIds();

            return (ActualNumberOfBookingIds: actualNumberOfBookingIds, ExpectedNumberOfBookingIds: expectedNumberOfBookingIds,
                ExpectedBookingIds: expectedBookingIds, BookingsIdsResponse: bookingsIdsResponse);
        }

        private async Task<IEnumerable<BookingModel>> GetBookingsById(int[] bookingsIds)
        {
            IList<BookingModel> bookingModels = new List<BookingModel>();
            for (int i = 0; i < bookingsIds.Count(); i++)
            {
                _request.RemoveBodyParameter(i, index: 2);
                _request.GetBookingByIdParameterRequest(bookingsIds[i]);
                var response = await _client.ExecuteAsync<BookingResponse>(_request);
                var result = JsonSerializer.Deserialize<BookingModel>(response.Content);
                bookingModels.Add(result);
            }

            return bookingModels;
        }
    }
}
