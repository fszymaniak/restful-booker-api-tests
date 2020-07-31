using System.Collections.Generic;
using System.Linq;
using RestfulBooker.ApiTests.Models;
using Shouldly;

namespace RestfulBooker.ApiTests.Extensions
{
    public static class ShouldlyExtension
    {
        public static void ShouldBeValid(this BookingModel bookingModel, BookingResponse bookingResponse)
        {
            bookingModel.ShouldSatisfyAllConditions(
                () => bookingModel.FirstName.ShouldBe(bookingResponse.Booking.FirstName),
                () => bookingModel.LastName.ShouldBe(bookingResponse.Booking.LastName),
                () => bookingModel.DepositPaid.ShouldBe(bookingResponse.Booking.DepositPaid),
                () => bookingModel.BookinDates.CheckIn.ShouldBe(bookingResponse.Booking.BookinDates.CheckIn),
                () => bookingModel.BookinDates.CheckOut.ShouldBe(bookingResponse.Booking.BookinDates.CheckOut),
                () => bookingModel.AdditionalNeeds.ShouldBe(bookingResponse.Booking.AdditionalNeeds));
        }

        public static void ShouldBeValid(this BookingResponse bookingResponse, BookingModel bookingModel)
        {
            bookingResponse.ShouldSatisfyAllConditions(
                () => bookingResponse.Booking.FirstName.ShouldBe(bookingModel.FirstName),
                () => bookingResponse.Booking.LastName.ShouldBe(bookingModel.LastName),
                () => bookingResponse.Booking.DepositPaid.ShouldBe(bookingModel.DepositPaid),
                () => bookingResponse.Booking.BookinDates.CheckIn.ShouldBe(bookingModel.BookinDates.CheckIn),
                () => bookingResponse.Booking.BookinDates.CheckOut.ShouldBe(bookingModel.BookinDates.CheckOut),
                () => bookingResponse.Booking.AdditionalNeeds.ShouldBe(bookingModel.AdditionalNeeds));
        }

        public static void ShouldBeValid(this BookingModel actualBookingModel, BookingModel expectedBookingResponse)
        {
            actualBookingModel.ShouldSatisfyAllConditions(
                () => actualBookingModel.FirstName.ShouldBe(expectedBookingResponse.FirstName),
                () => actualBookingModel.LastName.ShouldBe(expectedBookingResponse.LastName),
                () => actualBookingModel.DepositPaid.ShouldBe(expectedBookingResponse.DepositPaid),
                () => actualBookingModel.BookinDates.CheckIn.ShouldBe(expectedBookingResponse.BookinDates.CheckIn),
                () => actualBookingModel.BookinDates.CheckOut.ShouldBe(expectedBookingResponse.BookinDates.CheckOut),
                () => actualBookingModel.AdditionalNeeds.ShouldBe(expectedBookingResponse.AdditionalNeeds));
        }
    }
}
