using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Transformations;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Steps
{
    public class PatchBookingSteps : BookingTestBase
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly RestRequest _request = RestRequestExtension.Create(Endpoints.GetBookingByIdEndpoint, Method.PATCH);

        public PatchBookingSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        //[When(@"PATCH Booking single row request object (.*) with value (.*) is sent")]
        //public async Task WhenPATCHBookingRequestWithFirstNameObjectIsSent(string singleObjectName, string value)
        //{
        //    var bookingRequests = _scenarioContext.GetExpectedBookings().FirstOrDefault();
        //    var patchResponses = new List<IRestResponse<BookingResponse>>();
        //    var bookingId = _scenarioContext.GetBookingsIds().FirstOrDefault();
        //    var requestObject = BookingModelTransformations.TransformToSingleRequestObject(singleObjectName, value);
        //    BookingModelTransformations.ChangeExpectedBookingModel(bookingRequests, singleObjectName, value);

        //    _request.UpdateBookingByIdRequest(requestObject, bookingId, Method.PATCH);
        //    var patchResponse = await _client.ExecuteAsync<BookingResponse>(_request);
        //    patchResponses.Add(patchResponse);

        //    if (patchResponse.IsSuccessful)
        //    {
        //        var result = JsonSerializer.Deserialize<BookingResponse>(patchResponse.Content);
        //    }

        //    _scenarioContext.SetBookingResponses(patchResponses);
        //}

        [When(@"PATCH Booking with request from json file '(.*)' is sent")]
        public async Task WhenPatchBookingWithRquestFromJsonFileIsSentAsync(string filePath)
        {
            var requestObject = await BookingTestBase.LoadJsonFile<object>(filePath);
            var bookingId = _scenarioContext.GetBookingsIds().FirstOrDefault();
            var patchResponses = new List<IRestResponse<BookingResponse>>();

            _request.UpdateBookingByIdRequest(requestObject, bookingId, Method.PATCH);
            var patchResponse = await _client.ExecuteAsync<BookingResponse>(_request);
            patchResponses.Add(patchResponse);

            if (patchResponse.IsSuccessful)
            {
                var result = JsonSerializer.Deserialize<BookingModel>(patchResponse.Content);
                _scenarioContext.SetActualUpdatedBooking(result);
            }

            _scenarioContext.SetRestBookingResponses(patchResponses);
        }

        [Then(@"actual booking is compared with Booking Model from file '(.*)'")]
        public async Task ThenActualBookingIsComparedWithBookingModelFromFileAsync(string filePath)
        {
            var expectedBookingModel = await BookingTestBase.LoadJsonFile<BookingModel>(filePath);

            var actualBookingModel = _scenarioContext.GetActualUpdatedBooking();

            actualBookingModel.ShouldBeValid(expectedBookingModel);
        }
    }
}
