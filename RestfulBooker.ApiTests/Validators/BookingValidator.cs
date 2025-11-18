using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Models.Responses;
using Shouldly;
using System.Net;

namespace RestfulBooker.ApiTests.Validators
{
    public static class BookingValidator
    {
        public static void ValidateBookingResponse(BookingResponse response, BookingModel expectedModel)
        {
            response.ShouldNotBeNull();
            response.BookingId.ShouldBeGreaterThan(0);
            response.Booking.ShouldNotBeNull();

            ValidateBookingModel(response.Booking, expectedModel);
        }

        public static void ValidateBookingModel(BookingModel actual, BookingModel expected)
        {
            actual.ShouldNotBeNull();

            actual.ShouldSatisfyAllConditions(
                () => actual.FirstName.ShouldBe(expected.FirstName),
                () => actual.LastName.ShouldBe(expected.LastName),
                () => actual.TotalPrice.ShouldBe(expected.TotalPrice),
                () => actual.DepositPaid.ShouldBe(expected.DepositPaid),
                () => actual.AdditionalNeeds.ShouldBe(expected.AdditionalNeeds)
            );

            ValidateBookingDates(actual.BookingDates, expected.BookingDates);
        }

        public static void ValidateBookingDates(BookingDates actual, BookingDates expected)
        {
            if (expected == null)
            {
                actual.ShouldBeNull();
                return;
            }

            actual.ShouldNotBeNull();
            actual.ShouldSatisfyAllConditions(
                () => actual.CheckIn.ShouldBe(expected.CheckIn),
                () => actual.CheckOut.ShouldBe(expected.CheckOut)
            );
        }

        public static void ValidateStatusCode(HttpStatusCode actual, HttpStatusCode expected, string message = null)
        {
            actual.ShouldBe(expected, message ?? $"Expected status code {expected} but got {actual}");
        }

        public static void ValidateBookingExists(BookingModel booking)
        {
            booking.ShouldNotBeNull("Booking should exist");
            booking.FirstName.ShouldNotBeNullOrEmpty("FirstName should not be null or empty");
            booking.LastName.ShouldNotBeNullOrEmpty("LastName should not be null or empty");
        }
    }
}
