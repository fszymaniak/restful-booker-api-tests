using RestfulBooker.ApiTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests.Transformations
{
    public static class BookingModelTransformations
    {
        public static IEnumerable<object> TransformToBookingModelWithoutExcludedRow(string excludedRow, Table table)
        {
            return excludedRow switch
            {
                "FirstName" => table.Rows
                                       .Select(r => new
                                       {
                                           LastName = r["LastName"],
                                           TotalPrice = int.Parse(r["TotalPrice"]),
                                           DepositPaid = bool.Parse(r["DepositPaid"]),
                                           BookingDates = GetBookingDates(r["BookingDates"]),
                                           AdditionalNeeds = r["AdditionalNeeds"]
                                       }).ToList(),
                "LastName" => table.Rows
                                    .Select(r => new
                                    {
                                        FirstName = r["FirstName"],
                                        TotalPrice = int.Parse(r["TotalPrice"]),
                                        DepositPaid = bool.Parse(r["DepositPaid"]),
                                        BookingDates = GetBookingDates(r["BookingDates"]),
                                        AdditionalNeeds = r["AdditionalNeeds"]
                                    }).ToList(),
                "BookingDates" => table.Rows
                                    .Select(r => new
                                    {
                                        FirstName = r["FirstName"],
                                        LastName = r["LastName"],
                                        TotalPrice = int.Parse(r["TotalPrice"]),
                                        DepositPaid = bool.Parse(r["DepositPaid"]),
                                        AdditionalNeeds = r["AdditionalNeeds"]
                                    }).ToList(),
                "AdditionalNeeds" => table.Rows
                                       .Select(r => new
                                       {
                                           FirstName = r["FirstName"],
                                           LastName = r["LastName"],
                                           TotalPrice = int.Parse(r["TotalPrice"]),
                                           DepositPaid = bool.Parse(r["DepositPaid"]),
                                           BookingDates = GetBookingDates(r["BookingDates"]),
                                       }).ToList(),
                "TotalPrice" => table.Rows
                                    .Select(r => new
                                    {
                                        FirstName = r["FirstName"],
                                        LastName = r["LastName"],
                                        DepositPaid = bool.Parse(r["DepositPaid"]),
                                        BookingDates = GetBookingDates(r["BookingDates"]),
                                        AdditionalNeeds = r["AdditionalNeeds"]
                                    }).ToList(),
                "DepositPaid" => table.Rows
                                    .Select(r => new
                                    {
                                        FirstName = r["FirstName"],
                                        LastName = r["LastName"],
                                        TotalPrice = int.Parse(r["TotalPrice"]),
                                        BookingDates = GetBookingDates(r["BookingDates"]),
                                        AdditionalNeeds = r["AdditionalNeeds"]
                                    }).ToList(),
                _ => throw new ArgumentOutOfRangeException(excludedRow)
            };
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
