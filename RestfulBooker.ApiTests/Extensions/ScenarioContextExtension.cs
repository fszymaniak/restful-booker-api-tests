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
        private const string BookingIdsResponse = nameof(BookingIdsResponse);
        private const string BookingModels = nameof(BookingModels);
        private const string BookingResponses = nameof(BookingResponses);
        private const string ExpectedObjects = nameof(ExpectedObjects);
        private const string ActualUpdatedBooking = nameof(ActualUpdatedBooking);


        public static void SetExpectedBookings(this ScenarioContext scenarioContext, IEnumerable<BookingModel> bookingModels)
        {
            scenarioContext[ExpectedBookings] = bookingModels;
        }

        public static void SetExpectedObjects(this ScenarioContext scenarioContext, IEnumerable<object> bookingModels)
        {
            scenarioContext[ExpectedObjects] = bookingModels;
        }

        public static void SetActualUpdatedBooking(this ScenarioContext scenarioContext, BookingModel updatedBookingModel)
        {
            scenarioContext[ActualUpdatedBooking] = updatedBookingModel;
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
            scenarioContext[BookingIdsResponse] = bookingsIdsResponse;
        }

        public static void SetBookingModels(this ScenarioContext scenarioContext, IEnumerable<BookingModel> bookingModels)
        {
            scenarioContext[BookingModels] = bookingModels;
        }

        public static void SetRestBookingResponses (this ScenarioContext scenarioContext, IEnumerable<IRestResponse<BookingResponse>> bookingResponses)
        {
            scenarioContext[BookingResponses] = bookingResponses;
        }

        public static IEnumerable<BookingModel> GetExpectedBookings(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<BookingModel>)scenarioContext[ExpectedBookings];
        }

        public static IEnumerable<object> GetExpectedObjects(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<object>)scenarioContext[ExpectedObjects];
        }

        public static IEnumerable<BookingModel> GetExpectedUpdatedBookings(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<BookingModel>)scenarioContext[ExpectedUpdatedBookings];
        }

        public static IEnumerable<int> GetBookingsIds(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<int>)scenarioContext[BookingsIds];
        }

        public static IEnumerable<BookingModel> GetBookingModels(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<BookingModel>)scenarioContext[BookingModels];
        }

        public static IEnumerable<IRestResponse<BookingResponse>> GetRestBookingResponses(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<IRestResponse<BookingResponse>>)scenarioContext[BookingResponses];
        }

        public static int GetInitialNumberOfBookingIds(this ScenarioContext scenarioContext)
        {
            return (int)scenarioContext[InitialNumberOfBookingIds];
        }

        public static IEnumerable<BookingIdsResponse> GetBookingIdsResponses(this ScenarioContext scenarioContext)
        {
            return (IEnumerable<BookingIdsResponse>)scenarioContext[BookingIdsResponse];
        }

        public static BookingModel GetActualUpdatedBooking(this ScenarioContext scenarioContext)
        {
            return (BookingModel)scenarioContext[ActualUpdatedBooking];
        }
    }
}
