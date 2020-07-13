using System;
using RestfulBooker.ApiTests.Models;

namespace RestfulBooker.ApiTests.TestData
{
    public static class TestBookingModels
    {
        public static BookingModel ValidBookingModel = new BookingModel()
        {
            FirstName = "John",
            LastName = "Wick",
            TotalPrice = 2000,
            DepositPaid = false,
            BookinDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(5).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(12).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Dinner"
        };

        public static BookingModel BookingModelWithoutFirstName = new BookingModel()
        {
            LastName = "Smith",
            TotalPrice = 2500,
            DepositPaid = true,
            BookinDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(5).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(12).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithoutLastName = new BookingModel()
        {
            FirstName = "James",
            TotalPrice = 2500,
            DepositPaid = true,
            BookinDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(5).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(12).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithoutTotalPrice = new BookingModel()
        {
            FirstName = "James",
            LastName = "Smith",
            DepositPaid = true,
            BookinDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(5).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(12).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithoutDepositPaid = new BookingModel()
        {
            FirstName = "James",
            LastName = "Smith",
            TotalPrice = 2500,
            BookinDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(5).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(12).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithoutCheckIn = new BookingModel()
        {
            FirstName = "James",
            LastName = "Smith",
            TotalPrice = 2500,
            BookinDates = new BookingDates()
            {
                CheckOut = DateTime.Now.AddDays(12).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithoutCheckOut = new BookingModel()
        {
            FirstName = "James",
            LastName = "Smith",
            TotalPrice = 2500,
            BookinDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(5).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithoutBookingDates = new BookingModel()
        {
            FirstName = "James",
            LastName = "Smith",
            TotalPrice = 2500,
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithoutAdditionalNeeds = new BookingModel()
        {
            FirstName = "James",
            LastName = "Wick",
            TotalPrice = 2000,
            DepositPaid = false,
            BookinDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(5).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(12).ToString("yyyy/MM/dd")
            },
        };

        public static BookingModel[] InvalidBookingModels()
        {
            return new BookingModel[]
            {
                BookingModelWithoutFirstName,
                BookingModelWithoutLastName, 
                BookingModelWithoutCheckIn,
                BookingModelWithoutCheckOut,
                BookingModelWithoutBookingDates,
            };
        }
    }
}
