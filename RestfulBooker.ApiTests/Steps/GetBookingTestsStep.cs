using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        private static readonly IDictionary<string, Method> _endpointWithMethod = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, Method.GET } };
        private readonly RestRequest _request = RestRequestExtension.Create(_endpointWithMethod);
        private readonly ScenarioContext _scenarioContext;

        public GetBookingTestsStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [AfterScenario]
        private async Task CleanUp()
        {
            var bookingsIds = _scenarioContext.GetBookingsIds();

            await DeleteBookingsByIds(_request, bookingsIds);
        }

        [Given(@"bookings exist")]
        public async Task GivenBookingsExist(IList<BookingModel> bookingModels)
        {
            var bookings = await CreateBookings(bookingModels);

            var bookingsIds = bookings
                .Select(b => b.BookingId)
                .ToList();

            _scenarioContext.SetExpectedBookings(bookingModels);
            _scenarioContext.SetBookingsIds(bookingsIds);
        }

        [When(@"GET Booking by Id request is sent")]
        public async Task WhenGETBookingByIdRequestIsSent()
        {
            var bookingsIds = _scenarioContext.GetBookingsIds();

            var bookingResponses = await GetBookingsResultsById<BookingModel>(bookingsIds);

            _scenarioContext.SetBookingModelResponses(bookingResponses);
        }

        [When(@"GET Booking by Id request returns booking response")]
        public async Task WhenGETBookingByIdRequestReturnsBookingResponse()
        {
            var bookingsIds = _scenarioContext.GetBookingsIds();

            var bookingResponses = await GetBookingsResultsById<BookingResponse>(bookingsIds);

            _scenarioContext.SetBookingResponses(bookingResponses);
        }


        [Then(@"expected bookings should be valid to booking responses")]
        public void ThenExpectedBookingsShouldBeValidToBookingResponses()
        {
            var bookingModelResponses = _scenarioContext.GetBookingModelResponses();

            var expectedBookings = _scenarioContext.GetExpectedBookings();

            expectedBookings.ShouldBeValid(bookingModelResponses);
        }

        [Then(@"expected bookings should return expected status code (.*)")]
        public void ThenExpectedBookingsShouldReturnExpectedStatusCode(int p0)
        {
            var bookingModelResponses = _scenarioContext.GetBookingResponses();

            //bookingModelResponses
        }


        //private async Task<IEnumerable<BookingModel>> GetBookingsById(IEnumerable<int> bookingsIds)
        //{
        //    IList<BookingModel> bookingModels = new List<BookingModel>();
        //    foreach (var id in bookingsIds)
        //    {
        //        _request.BookingByIdRequest(id, Method.GET);
        //        var response = await _client.ExecuteAsync<BookingResponse>(_request);
        //        var result = JsonSerializer.Deserialize<BookingModel>(response.Content);
        //        bookingModels.Add(result);
        //    }

        //    return bookingModels;
        //}

        private async Task<IEnumerable<T>> GetBookingsResultsById<T>(IEnumerable<int> bookingsIds) where T: class, new()
        {
            var type = new T();
            
            IList<T> bookingModels = new List<T>();
            
            foreach (var id in bookingsIds)
            {
                _request.BookingByIdRequest(id, Method.GET);
                var response = await _client.ExecuteAsync<BookingResponse>(_request);

                switch (type)
                {
                    case BookingModel _:
                    {
                        var result = JsonSerializer.Deserialize<T>(response.Content);
                        bookingModels.Add((T)result);
                        break;
                    }
                    case BookingResponse _:
                        bookingModels.Add((T)(IRestResponse<T>)response);
                        break;
                }
                
            }

            return bookingModels;
        }
    }
}
