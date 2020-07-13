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
    }
}
