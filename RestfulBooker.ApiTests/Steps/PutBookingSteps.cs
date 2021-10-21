using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Extensions;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using RestSharp;
using System.Threading.Tasks;
using System;
using RestfulBooker.ApiTests.Transformations;

namespace RestfulBooker.ApiTests.Steps
{
    [Binding]
    public class PutBookingSteps : BookingTestBase
    {
        private readonly ScenarioContext _scenarioContext; 

        public PutBookingSteps(ScenarioContext scenarioContext)
        { 
            _scenarioContext = scenarioContext;
        }

        [When(@"(.*) Bookings request with following data is sent")]
        public async Task WhenUpdateBookingsRequestWithFollowingDataIsSent(string method, IList<BookingModel> updatedBookingModels)
        {
            var bookingIds = _scenarioContext.GetBookingsIds();

            var updateMethod = GetUpdateMethod(method);
            await UpdateBookingById(updatedBookingModels, bookingIds, updateMethod);

            _scenarioContext.SetBookingModels(updatedBookingModels);
        }

        [When(@"PUT Bookings request with invalid data without (.*) is sent")]
        public async Task WhenPutBookingsRequestWithInvalidDataIsSent(string excludedRow, Table bookingModelsTable)
        {
            var bookingModels = BookingModelTransformations.TransformToBookingModelWithoutExcludedRow(excludedRow, bookingModelsTable);
            var bookingIds = _scenarioContext.GetBookingsIds();

            var putBookingsResponses = await UpdateBookingByIdResponse(bookingModels, bookingIds, Method.PUT);

            _scenarioContext.SetBookingResponses(putBookingsResponses);
        }

        private Method GetUpdateMethod(string method)
        {
            return method switch
            {
                "PUT" => Method.PUT,
                "PATCH" => Method.PATCH,
                _ => throw new ArgumentOutOfRangeException(method)
            };
        }
    }
}
