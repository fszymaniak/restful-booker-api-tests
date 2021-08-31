using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Transformations;
using RestSharp;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Steps
{
    [Binding]
    public class PostBookingSteps : BookingTestBase
    {
        private ScenarioContext _scenarioContext;

        private readonly RestRequest _request = RestRequestExtension.Create(Endpoints.BookingEndpoint, Method.POST);

        public PostBookingSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"valid bookings models exist")]
        public void GivenValidBookingsModelsExist(IList<BookingModel> bookingModels)
        {
            _scenarioContext.SetExpectedBookings(bookingModels);
        }

        [Given(@"valid bookings models without (.*) exist")]
        public void GivenValidBookingsModelsWithoutAdditionalNeedsExist(string excludedRow, Table bookingModels)
        {
            var expectedBookingModels = BookingModelTransformations.TransformToBookingModelWithoutExcludedRow(excludedRow, bookingModels);
            _scenarioContext.SetExpectedBookings(expectedBookingModels);
        }

        [Given(@"invalid booking model without (.*) exists")]
        public void GivenInvalidBookingModelExists(string excludedRow, Table bookingModels)
        {
            var expectedBookingModels = BookingModelTransformations.TransformToBookingModelWithoutExcludedRow(excludedRow, bookingModels);
            _scenarioContext.SetExpectedBookings(expectedBookingModels);
        }

        [When(@"POST Bookings request is sent")]
        public async Task WhenPOSTBookingsRequestIsSent()
        {
            var bookingRequests = _scenarioContext.GetExpectedBookings();
            var postResponses = new List<IRestResponse<BookingResponse>>();
            var bookingIds = new List<int>();

            foreach (var request in bookingRequests)
            {
                _request.PostBookingRequest(request);
                var postResponse = await _client.ExecuteAsync<BookingResponse>(_request);
                postResponses.Add(postResponse);

                if(postResponse.IsSuccessful)
                {
                    var result = JsonSerializer.Deserialize<BookingResponse>(postResponse.Content);
                    bookingIds.Add(result.BookingId);
                }
            }

            _scenarioContext.SetBookingResponses(postResponses);
            _scenarioContext.SetBookingsIds(bookingIds);
        }
    }
}
