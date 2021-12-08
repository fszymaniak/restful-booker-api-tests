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

        //public static object TransformToSingleRequestObject(string propertyName, string propertyValue)
        //{
        //    return propertyName switch
        //    {
        //        "FirstName" => new { FirstName = propertyValue  },
        //        "LastName" => new { LastName = propertyValue },
        //        "BookingDates" => new { BookingDates = GetBookingDates(propertyValue) },
        //        "AdditionalNeeds" => new { AdditionalNeeds = propertyValue },
        //        "TotalPrice" => new { TotalPrice = int.Parse(propertyValue) },
        //        "DepositPaid" => new { DepositPaid = bool.Parse(propertyValue) },
        //        _ => throw new ArgumentOutOfRangeException(propertyName)
        //    };
        //}

        //public static void ChangeExpectedBookingModel(BookingModel bookingModel, string propertyName, string propertyValue)
        //{
        //    switch(propertyName)
        //    {
        //        case "FirstName":
        //            bookingModel.FirstName = propertyValue;
        //            break;
        //        case "LastName":
        //            bookingModel.LastName = propertyValue;
        //            break;
        //        case "BookingDates":
        //            bookingModel.BookingDates = GetBookingDates(propertyValue);
        //            break;
        //        case "AdditionalNeeds":
        //            bookingModel.AdditionalNeeds = propertyValue;
        //            break;
        //        case "TotalPrice":
        //            bookingModel.TotalPrice = int.Parse(propertyValue);
        //            break;
        //        case "DepositPaid":
        //            bookingModel.DepositPaid = bool.Parse(propertyValue);
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException(propertyName);
        //    };
        //}

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
