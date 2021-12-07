using System;
using System.Linq;
using System.Threading.Tasks;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Extensions;
using RestSharp;
using TechTalk.SpecFlow;


namespace RestfulBooker.ApiTests.Hooks
{
    [Binding]
    public class Hooks : BookingTestBase
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly RestRequest _request = RestRequestExtension.Create(Endpoints.GetBookingByIdEndpoint, Method.GET);

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [AfterScenario("TestDataCleanup")]
        private async Task CleanUp()
        {
            var bookingsIds = _scenarioContext.GetBookingsIds();

            await DeleteBookingsByIds(_request, bookingsIds);

            DisposeSpecFlowContext();
        }

        [BeforeScenario("GetInitialBookingIds")]
        private async Task GetInitialNumberOfBookingIds()
        {
            var bookingsIdsResponse = await GetBookingIds();

            _scenarioContext.SetInitialNumberOfBookingIds(bookingsIdsResponse.Count());
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
