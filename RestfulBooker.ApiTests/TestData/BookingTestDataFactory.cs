using System;
using RestfulBooker.ApiTests.Models;

namespace RestfulBooker.ApiTests.TestData
{
    /// <summary>
    /// Factory for creating fresh test data instances for better test isolation.
    /// Use this instead of static fields to ensure each test gets independent data.
    /// </summary>
    public static class BookingTestDataFactory
    {
        private static readonly DateTime BaseDate = new DateTime(2024, 12, 1);

        public static BookingModel CreateValidBooking()
        {
            return new BookingModel
            {
                FirstName = "John",
                LastName = "Wick",
                TotalPrice = 2000,
                DepositPaid = false,
                BookingDates = new BookingDates
                {
                    CheckIn = BaseDate.AddDays(5).ToString("yyyy-MM-dd"),
                    CheckOut = BaseDate.AddDays(12).ToString("yyyy-MM-dd")
                },
                AdditionalNeeds = "Dinner"
            };
        }

        public static BookingModel CreateValidBookingWithUpdates()
        {
            return new BookingModel
            {
                FirstName = "John",
                LastName = "Wick",
                TotalPrice = 3000,
                DepositPaid = true,
                BookingDates = new BookingDates
                {
                    CheckIn = BaseDate.AddDays(10).ToString("yyyy-MM-dd"),
                    CheckOut = BaseDate.AddDays(17).ToString("yyyy-MM-dd")
                },
                AdditionalNeeds = "Supper"
            };
        }

        public static BookingModel CreateBookingWithoutFirstName()
        {
            return new BookingModel
            {
                FirstName = null,
                LastName = "Smith",
                TotalPrice = 2500,
                DepositPaid = true,
                BookingDates = new BookingDates
                {
                    CheckIn = BaseDate.AddDays(5).ToString("yyyy-MM-dd"),
                    CheckOut = BaseDate.AddDays(12).ToString("yyyy-MM-dd")
                },
                AdditionalNeeds = "Launch"
            };
        }

        public static BookingModel CreateBookingWithoutLastName()
        {
            return new BookingModel
            {
                FirstName = "James",
                LastName = null,
                TotalPrice = 2500,
                DepositPaid = true,
                BookingDates = new BookingDates
                {
                    CheckIn = BaseDate.AddDays(5).ToString("yyyy-MM-dd"),
                    CheckOut = BaseDate.AddDays(12).ToString("yyyy-MM-dd")
                },
                AdditionalNeeds = "Launch"
            };
        }

        public static BookingModel CreateBookingWithoutCheckIn()
        {
            return new BookingModel
            {
                FirstName = "James",
                LastName = "Smith",
                TotalPrice = 2500,
                DepositPaid = true,
                BookingDates = new BookingDates
                {
                    CheckIn = null,
                    CheckOut = BaseDate.AddDays(12).ToString("yyyy-MM-dd")
                },
                AdditionalNeeds = "Launch"
            };
        }

        public static BookingModel CreateBookingWithoutCheckOut()
        {
            return new BookingModel
            {
                FirstName = "James",
                LastName = "Smith",
                TotalPrice = 2500,
                DepositPaid = true,
                BookingDates = new BookingDates
                {
                    CheckIn = BaseDate.AddDays(5).ToString("yyyy-MM-dd"),
                    CheckOut = null
                },
                AdditionalNeeds = "Launch"
            };
        }

        public static BookingModel CreateBookingWithoutBookingDates()
        {
            return new BookingModel
            {
                FirstName = "James",
                LastName = "Smith",
                TotalPrice = 2500,
                DepositPaid = true,
                BookingDates = null,
                AdditionalNeeds = "Launch"
            };
        }

        public static BookingModel CreateBookingWithoutAdditionalNeeds()
        {
            return new BookingModel
            {
                FirstName = "James",
                LastName = "Wick",
                TotalPrice = 2000,
                DepositPaid = false,
                BookingDates = new BookingDates
                {
                    CheckIn = BaseDate.AddDays(5).ToString("yyyy-MM-dd"),
                    CheckOut = BaseDate.AddDays(12).ToString("yyyy-MM-dd")
                },
                AdditionalNeeds = null
            };
        }

        public static BookingModel[] GetInvalidBookingModels()
        {
            return new[]
            {
                CreateBookingWithoutFirstName(),
                CreateBookingWithoutLastName(),
                CreateBookingWithoutCheckIn(),
                CreateBookingWithoutCheckOut(),
                CreateBookingWithoutBookingDates()
            };
        }
    }
}
