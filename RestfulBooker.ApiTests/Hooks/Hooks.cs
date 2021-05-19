using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulBooker.ApiTests.Extensions;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Hooks
{
    [Binding]
    public class Hooks : BookingTestBase
    {
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario("GetInitialBookingIds")]
        private async Task GetInitialNumberOfBookingIds()
        {
            var bookingsIdsResponse = await GetBookingIds();

            _scenarioContext.SetInitialNumberOfBookingIds(bookingsIdsResponse.Count());
        }
    }
}
