using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Models.Responses;
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

        //[AfterScenario]
        //private async Task CleanUp()
        //{
        //    var bookingsIds = _scenarioContext.GetBookingsIds();

        //    await DeleteBookingsByIds(_request, bookingsIds);
        //}

        //[Given(@"bookings exist")]
        //public async Task GivenBookingsExist(IList<BookingModel> bookingModels)
        //{
        //    var bookings = await CreateBookings(bookingModels);

        //    var bookingsIds = bookings
        //        .Select(b => b.BookingId)
        //        .ToList();

        //    _scenarioContext.SetExpectedBookings(bookingModels);

        //    _scenarioContext.SetBookingsIds(bookingsIds);
        //}

        [Given(@"not existing bookings")]
        public void GivenNotExisting(IEnumerable<int> notExistingBookingId)
        {
            _scenarioContext.SetBookingsIds(notExistingBookingId);
        }

        [When(@"GET Booking by Id request is sent")]
        public async Task WhenGetBookingByIdRequestIsSent()
        {
            var bookingsIds = _scenarioContext.GetBookingsIds();

            var bookingResponses = await GetBookingsById(bookingsIds);

            _scenarioContext.SetBookingModelResponses(bookingResponses);
        }

        [When(@"GET Booking by Id request returns booking response")]
        public async Task WhenGetBookingByIdRequestReturnsBookingResponse()
        {
            var bookingsIds = _scenarioContext.GetBookingsIds();

            var bookingResponses = await GetBookingsResponsesById(bookingsIds);

            _scenarioContext.SetRestBookingResponses(bookingResponses);
        }


        [Then(@"expected bookings should be valid to booking responses")]
        public void ThenExpectedBookingsShouldBeValidToBookingResponses()
        {
            var bookingModelResponses = _scenarioContext.GetBookingModelResponses();

            var expectedBookings = _scenarioContext.GetExpectedBookings();

            expectedBookings.ShouldBeValid(bookingModelResponses);
        }

        [Then(@"expected bookings should return expected status code (.*)")]
        public void ThenExpectedBookingsShouldReturnExpectedStatusCode(HttpStatusCode statusCode)
        {
            var bookingModelResponses = _scenarioContext.GetRestBookingResponses();

            bookingModelResponses.ShouldHaveValidStatusCode(statusCode);
        }

        private async Task<IEnumerable<BookingModel>> GetBookingsById(IEnumerable<int> bookingsIds)
        {
            IList<BookingModel> bookingModels = new List<BookingModel>();
            foreach (var id in bookingsIds)
            {
                _request.BookingByIdRequest(id, Method.GET);
                var response = await _client.ExecuteAsync<BookingResponse>(_request);
                var result = JsonSerializer.Deserialize<BookingModel>(response.Content);
                bookingModels.Add(result);
            }

            return bookingModels;
        }

        private async Task<IEnumerable<IRestResponse<BookingResponse>>> GetBookingsResponsesById(IEnumerable<int> bookingsIds)
        {
            IList<IRestResponse<BookingResponse>> bookingResponse = new List<IRestResponse<BookingResponse>>();
            foreach (var id in bookingsIds)
            {
                _request.BookingByIdRequest(id, Method.GET);
                var response = await _client.ExecuteAsync<BookingResponse>(_request);
                bookingResponse.Add(response);
            }

            return bookingResponse;
        }
    }
}
