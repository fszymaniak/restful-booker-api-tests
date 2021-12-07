using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Transformations;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Steps
{
    [Binding]
    public class PostBookingSteps : BookingTestBase
    {
        private readonly ScenarioContext _scenarioContext;

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
            _scenarioContext.SetExpectedObjects(expectedBookingModels);
        }

        [Given(@"invalid bookings models without (.*) exists")]
        public void GivenInvalidBookingModelExists(string excludedRow, Table bookingModels)
        {
            var expectedBookingModels = BookingModelTransformations.TransformToBookingModelWithoutExcludedRow(excludedRow, bookingModels);
            _scenarioContext.SetExpectedObjects(expectedBookingModels);
        }

        [When(@"(.*) Bookings request with (.*) object is sent")]
        public async Task WhenPostOrPatchBookingsRequestIsSent(string method, string isObjectComplete)
        {
            var bookingRequests = GetBookingObject(isObjectComplete);
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

            _scenarioContext.SetRestBookingResponses(postResponses);
            _scenarioContext.SetBookingsIds(bookingIds);
        }

        private IEnumerable<object> GetBookingObject(string isObjectComplete)
        {
            return isObjectComplete switch
            {
                "complete" => _scenarioContext.GetExpectedBookings(),
                "incomplete" => _scenarioContext.GetExpectedObjects(),
                _ => throw new ArgumentOutOfRangeException(isObjectComplete)
            };
        }
    }
}
