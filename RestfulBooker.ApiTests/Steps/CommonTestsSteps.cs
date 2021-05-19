using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Steps
{
    [Binding]
    public class CommonTestsSteps : BookingTestBase
    {

        private ScenarioContext _scenarioContext;

        private static readonly IDictionary<string, Method> _endpointWithMethod = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, Method.GET } };
        private readonly RestRequest _request = RestRequestExtension.Create(_endpointWithMethod);

        public CommonTestsSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [AfterScenario]
        private async Task CleanUp()
        {
            var bookingsIds = _scenarioContext.GetBookingsIds();

            await DeleteBookingsByIds(_request, bookingsIds);

            DisposeSpecFlowContext();
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

        private void DisposeSpecFlowContext()
        {
            try
            {
                var disposableContext = _scenarioContext as IDisposable;
                disposableContext.Dispose();
            }
            catch
            {
            }
        }
    }
}
