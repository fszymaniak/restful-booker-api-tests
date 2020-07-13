using NUnit.Framework;
using RestfulBooker.ApiTests.Extensions;
using System.Threading.Tasks;

namespace RestfulBooker.ApiTests.Api
{
    public class GetBookingTests
    {
        [Test]
        public async Task Get_Booking_WhenIdExists()
        {
            // given
            var booking = BookingTestBase.CreateBooking("Phil", "Collins", 1000, true, "2020-08-23", "2020-08-30", "Breakfasts");

            // when
            var results = BookingTestBase.GetBookingById(booking.BookingId);

            // then
            results.ShouldBeValid(booking);
        }
    }
}
