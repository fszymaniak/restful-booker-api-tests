using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestfulBooker.ApiTests.Models;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Extensions
{
    public static class ScenarioContextExtension
    {
        private const string ExpectedBookings = nameof(ExpectedBookings);
        private const string BookingsIds = nameof(BookingsIds);
        private const string BookingModelResponses = nameof(BookingModelResponses);
        private const string BookingResponses = nameof(BookingResponses);


        public static void SetExpectedBookings(this ScenarioContext scenarioContext, IEnumerable<BookingModel> bookingModels)
        {
            scenarioContext[ExpectedBookings] = bookingModels;
        }

        public static void SetBookingsIds(this ScenarioContext scenarioContext, IEnumerable<int> bookingsIds)
        {
            scenarioContext[BookingsIds] = bookingsIds;
        }

        public static void SetBookingModelResponses(this ScenarioContext scenarioContext, IEnumerable<BookingModel> bookingModelResponses)
        {
            scenarioContext[BookingModelResponses] = bookingModelResponses;
        }

        public static void SetBookingResponses(this ScenarioContext scenarioContext, IEnumerable<BookingResponse> bookingsResponses)
        {
            scenarioContext[BookingResponses] = bookingsResponses;
        }

        public static IEnumerable<BookingModel> GetExpectedBookings(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<BookingModel>)scenarioContext[ExpectedBookings];
        }

        public static IEnumerable<int> GetBookingsIds(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<int>)scenarioContext[BookingsIds];
        }

        public static IEnumerable<BookingModel> GetBookingModelResponses(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<BookingModel>)scenarioContext[BookingModelResponses];
        }

        public static IEnumerable<BookingResponse> GetBookingResponses(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<BookingResponse>)scenarioContext[BookingModelResponses];
        }

    }
}
