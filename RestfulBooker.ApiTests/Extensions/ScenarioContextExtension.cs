using System.Collections.Generic;
using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Models.Responses;
using RestSharp;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Extensions
{
    public static class ScenarioContextExtension
    {
        private const string ExpectedBookings = nameof(ExpectedBookings);
        private const string ExpectedUpdatedBookings = nameof(ExpectedUpdatedBookings);
        private const string BookingsIds = nameof(BookingsIds);
        private const string InitialNumberOfBookingIds = nameof(InitialNumberOfBookingIds);
        private const string BookingIdsResponses = nameof(BookingIdsResponses);
        private const string BookingModelResponses = nameof(BookingModelResponses);
        private const string RestBookingResponses = nameof(RestBookingResponses);


        public static void SetExpectedBookings(this ScenarioContext scenarioContext, IEnumerable<BookingModel> bookingModels)
        {
            scenarioContext[ExpectedBookings] = bookingModels;
        }

        public static void SetExpectedUpdatedBookings(this ScenarioContext scenarioContext, IEnumerable<BookingModel> bookingModels)
        {
            scenarioContext[ExpectedUpdatedBookings] = bookingModels;
        }

        public static void SetBookingsIds(this ScenarioContext scenarioContext, IEnumerable<int> bookingsIds)
        {
            scenarioContext[BookingsIds] = bookingsIds;
        }

        public static void SetInitialNumberOfBookingIds(this ScenarioContext scenarioContext, int bookingsResponse)
        {
            scenarioContext[InitialNumberOfBookingIds] = bookingsResponse;
        }

        public static void SetBookingIdsResponses(this ScenarioContext scenarioContext, IEnumerable<BookingIdsResponse> bookingsIdsResponse)
        {
            scenarioContext[BookingIdsResponses] = bookingsIdsResponse;
        }

        public static void SetBookingModelResponses(this ScenarioContext scenarioContext, IEnumerable<BookingModel> bookingModelResponses)
        {
            scenarioContext[BookingModelResponses] = bookingModelResponses;
        }

        public static void SetRestBookingResponses(this ScenarioContext scenarioContext, IEnumerable<IRestResponse<BookingResponse>> bookingsResponses)
        {
            scenarioContext[RestBookingResponses] = bookingsResponses;
        }

        public static IEnumerable<BookingModel> GetExpectedBookings(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<BookingModel>)scenarioContext[ExpectedBookings];
        }

        public static IEnumerable<BookingModel> GetExpectedUpdatedBookings(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<BookingModel>)scenarioContext[ExpectedUpdatedBookings];
        }

        public static IEnumerable<int> GetBookingsIds(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<int>)scenarioContext[BookingsIds];
        }

        public static IEnumerable<BookingModel> GetBookingModelResponses(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<BookingModel>)scenarioContext[BookingModelResponses];
        }

        public static IEnumerable<IRestResponse<BookingResponse>> GetRestBookingResponses(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<IRestResponse<BookingResponse>>)scenarioContext[RestBookingResponses];
        }

        public static int GetInitialNumberOfBookingIds(this ScenarioContext scenarioContext)
        {
            return (int)scenarioContext[InitialNumberOfBookingIds];
        }

        public static IEnumerable<BookingIdsResponse> GetBookingIdsResponses(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<BookingIdsResponse>)scenarioContext[BookingIdsResponses];
        }
    }
}
