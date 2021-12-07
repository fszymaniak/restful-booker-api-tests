using System.Collections.Generic;
using System.Threading.Tasks;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Steps
{
    [Binding]
    public class GetBookingTestsStep : BookingTestBase
    {
        private readonly RestRequest _request = RestRequestExtension.Create(Endpoints.GetBookingByIdEndpoint, Method.GET);
        protected readonly ScenarioContext _scenarioContext;

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
            var bookingModelResponses = _scenarioContext.GetBookingModels();

            var expectedBookings = _scenarioContext.GetExpectedBookings();

            expectedBookings.ShouldBeValid(bookingModelResponses);
        }
        
        private async IAsyncEnumerable<IRestResponse<BookingResponse>> GetBookingsResponsesById(IEnumerable<int> bookingsIds)
        {
            //IList<IRestResponse<BookingResponse>> bookingResponse = new List<IRestResponse<BookingResponse>>();
            foreach (var id in bookingsIds)
            {
                _request.BookingByIdRequest(id);
                var response = await _client.ExecuteAsync<BookingResponse>(_request);
                //bookingResponse.Add(response);
                yield return response;
            }

            //return bookingResponse;
        }
    }
}
