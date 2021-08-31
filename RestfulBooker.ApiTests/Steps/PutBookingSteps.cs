using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Extensions;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using RestSharp;
using System.Threading.Tasks;
using System;

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

            var bookings = new List<BookingModel>();
            var updateMethod = GetUpdateMethod(method);

            await foreach (var booking in UpdateBookingById(updatedBookingModels, bookingIds, updateMethod))
            {
                bookings.Add(booking);
            }

            _scenarioContext.SetExpectedUpdatedBookings(updatedBookingModels);
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
