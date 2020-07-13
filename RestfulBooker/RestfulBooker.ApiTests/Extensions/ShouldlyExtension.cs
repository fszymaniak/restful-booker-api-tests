using RestfulBooker.ApiTests.Models;
using Shouldly;

namespace RestfulBooker.ApiTests.Extensions
{
    public static class ShouldlyExtension
    {
        //public static void ShouldBeValid(this BookingResponse bookingResponse, BookingRequest bookingRequest)
        //{
        //    bookingResponse.ShouldSatisfyAllConditions(
        //        () => bookingResponse.Booking.FirstName.ShouldBe(bookingRequest.Booking.FirstName),
        //        () => bookingResponse.Booking.LastName.ShouldBe(bookingRequest.Booking.LastName),
        //        () => bookingResponse.Booking.DepositPaid.ShouldBe(bookingRequest.Booking.DepositPaid),
        //        () => bookingResponse.Booking.BookinDates.CheckIn.ShouldBe(bookingRequest.Booking.BookinDates.CheckIn),
        //        () => bookingResponse.Booking.BookinDates.CheckOut.ShouldBe(bookingRequest.Booking.BookinDates.CheckOut),
        //        () => bookingResponse.Booking.AdditionalNeeds.ShouldBe(bookingRequest.Booking.AdditionalNeeds));
        //}

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
    }
}
