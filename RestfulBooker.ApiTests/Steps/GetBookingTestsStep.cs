using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Steps
{
    [Parallelizable(ParallelScope.Fixtures)]
    [Binding]
    public class GetBookingTestsStep : BookingTestBase
    {
        private static readonly IDictionary<string, Method> _endpointWithMethod = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, Method.GET } };
        private readonly RestRequest _request = RestRequestExtension.Create(_endpointWithMethod);
        private readonly ScenarioContext _scenarioContext;

        public GetBookingTestsStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"not existing bookings")]
        public void GivenNotExisting(IEnumerable<int> notExistingBookingId)
        {
            _scenarioContext.SetBookingsIds(notExistingBookingId);
        }

        

        [When(@"GET Booking by Id request returns booking response")]
        public async Task WhenGetBookingByIdRequestReturnsBookingResponse()
        {
            var bookingsIds = _scenarioContext.GetBookingsIds();

            IList<IRestResponse<BookingResponse>> bookingResponses = new List<IRestResponse<BookingResponse>>();

            await foreach (var bookingResponse in GetBookingsResponsesById(bookingsIds))
            {
                bookingResponses.Add(bookingResponse);
            }

            _scenarioContext.SetRestBookingResponses(bookingResponses);
        }

        [Then(@"expected bookings should be valid to booking responses")]
        public void ThenExpectedBookingsShouldBeValidToBookingResponses()
        {
            var bookingModelResponses = _scenarioContext.GetBookingModelResponses();

            var expectedBookings = _scenarioContext.GetExpectedBookings();

            expectedBookings.ShouldBeValid(bookingModelResponses);
        }
        
        private async IAsyncEnumerable<IRestResponse<BookingResponse>> GetBookingsResponsesById(IEnumerable<int> bookingsIds)
        {
            //IList<IRestResponse<BookingResponse>> bookingResponse = new List<IRestResponse<BookingResponse>>();
            foreach (var id in bookingsIds)
            {
                _request.BookingByIdRequest(id, Method.GET);
                var response = await _client.ExecuteAsync<BookingResponse>(_request);
                //bookingResponse.Add(response);
                yield return response;
            }

            //return bookingResponse;
        }
    }
}
