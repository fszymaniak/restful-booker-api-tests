using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Models.Responses;
using RestSharp;
using Shouldly;

namespace RestfulBooker.ApiTests.Extensions
{
    public static class ShouldlyExtension
    {
        public static bool ShouldBeValid(this IEnumerable<BookingModel> bookingModel, IEnumerable<BookingModel> bookingResponse)
        {
            var bModel = bookingModel.ToList();
            var bResponse = bookingResponse.ToList();

            return bModel.Count == bResponse.Count 
                && bModel.All(bM => bResponse.Any(bR => ModelAndResponseShouldBeValid(bM, bR)));
        }

        public static bool ModelAndResponseShouldBeValid(BookingModel bM, BookingModel bR)
        {
            return bR.FirstName == bM.FirstName
            && bR.LastName == bM.LastName
            && bR.DepositPaid == bM.DepositPaid
            && bR.BookingDates.CheckIn == bM.BookingDates.CheckIn
            && bR.BookingDates.CheckOut == bM.BookingDates.CheckOut
            && bR.AdditionalNeeds == bM.AdditionalNeeds;
        }

        public static void ShouldHaveValidStatusCode(this IEnumerable<IRestResponse<BookingResponse>> bookingResponses, HttpStatusCode statusCode)
        {
            foreach (var response in bookingResponses)
            {
                response.StatusCode.ShouldBe(statusCode);
            }
        }

        public static bool ShouldIncludesBookingIds(this IEnumerable<BookingIdsResponse> bookingIdsResponses, IEnumerable<int> bookingIds)
        {
            var actualBookingIds = bookingIdsResponses.Select(bR => bR.BookingId).ToList();
            return bookingIds.All(id => actualBookingIds.Contains(id));
        }

        public static bool ShouldNotIncludesBookingIds(this IEnumerable<BookingIdsResponse> bookingIdsResponses, IEnumerable<int> bookingIds)
        {
            var actualBookingIds = bookingIdsResponses.Select(bR => bR.BookingId).ToList();
            return bookingIds.All(id => !actualBookingIds.Contains(id));
        }

        public static void ShouldBeValid(this BookingModel bookingModel, BookingResponse bookingResponse)
        {
            bookingModel.ShouldSatisfyAllConditions(
                () => bookingModel.FirstName.ShouldBe(bookingResponse.Booking.FirstName),
                () => bookingModel.LastName.ShouldBe(bookingResponse.Booking.LastName),
                () => bookingModel.DepositPaid.ShouldBe(bookingResponse.Booking.DepositPaid),
                () => bookingModel.BookingDates.CheckIn.ShouldBe(bookingResponse.Booking.BookingDates.CheckIn),
                () => bookingModel.BookingDates.CheckOut.ShouldBe(bookingResponse.Booking.BookingDates.CheckOut),
                () => bookingModel.AdditionalNeeds.ShouldBe(bookingResponse.Booking.AdditionalNeeds));
        }

        public static void ShouldBeValid(this BookingResponse bookingResponse, BookingModel bookingModel)
        {
            bookingResponse.ShouldSatisfyAllConditions(
                () => bookingResponse.Booking.FirstName.ShouldBe(bookingModel.FirstName),
                () => bookingResponse.Booking.LastName.ShouldBe(bookingModel.LastName),
                () => bookingResponse.Booking.DepositPaid.ShouldBe(bookingModel.DepositPaid),
                () => bookingResponse.Booking.BookingDates.CheckIn.ShouldBe(bookingModel.BookingDates.CheckIn),
                () => bookingResponse.Booking.BookingDates.CheckOut.ShouldBe(bookingModel.BookingDates.CheckOut),
                () => bookingResponse.Booking.AdditionalNeeds.ShouldBe(bookingModel.AdditionalNeeds));
        }

        public static void ShouldBeValid(this BookingModel actualBookingModel, BookingModel expectedBookingResponse)
        {
            actualBookingModel.ShouldSatisfyAllConditions(
                () => actualBookingModel.FirstName.ShouldBe(expectedBookingResponse.FirstName),
                () => actualBookingModel.LastName.ShouldBe(expectedBookingResponse.LastName),
                () => actualBookingModel.DepositPaid.ShouldBe(expectedBookingResponse.DepositPaid),
                () => actualBookingModel.BookingDates.CheckIn.ShouldBe(expectedBookingResponse.BookingDates.CheckIn),
                () => actualBookingModel.BookingDates.CheckOut.ShouldBe(expectedBookingResponse.BookingDates.CheckOut),
                () => actualBookingModel.AdditionalNeeds.ShouldBe(expectedBookingResponse.AdditionalNeeds));
        }
    }
}
