using System;
using RestfulBooker.ApiTests.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
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

        [StepArgumentTransformation(@"should return expected status code (\d+)")]
        public HttpStatusCode TransformToHttpStatusCode(int statusCode)
        {
            return (HttpStatusCode)statusCode;
        }

        [StepArgumentTransformation(@"not existing bookings")]
        public IEnumerable<int> TransformToListOfNotExistingBookingIds(Table table)
        {
            return table.Rows
                .Select(r => int.Parse(r["NotExistingBookingsIds"])).ToList();
        }

        private static BookingDates GetBookingDates(string bookingDates)
        {
            var parts = bookingDates.Split('/').Select(p => p.Trim()).ToList();
            var format = "yyyy-MM-dd";

            return new BookingDates()
            {
                CheckIn = ParseDateTimeFromString(parts.First(), format),
                CheckOut = ParseDateTimeFromString(parts.Last(), format)
            };
        }

        private static DateTime ParseDateTimeFromString(string str, string format)
        {
            return DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);
        }

    }
}
