using RestfulBooker.ApiTests.Models;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Transformations
{
    [Binding]
    public class Transformations
    {
        [StepArgumentTransformation]
        public IList<BookingModel> TransformToBookingModel(Table table)
        {
            return table.Rows
                .Select(r => new BookingModel { 
                FirstName = r["FirstName"],
                LastName = r["LastName"],
                TotalPrice = int.Parse(r["TotalPrice"]),
                DepositPaid = bool.Parse(r["DepositPaid"]),
                BookingDates = GetBookingDates(r["BookingDates"]),
                AdditionalNeeds = r["AdditionalNeeds"]
                }).ToList();
        }

        private static BookingDates GetBookingDates(string bookingDates)
        {
            var parts = bookingDates.Split('/').Select(p => p.Trim()).ToList();

            return new BookingDates()
            {
                CheckIn = parts.First(),
                CheckOut = parts.Last()
            };
        }

    }
}
