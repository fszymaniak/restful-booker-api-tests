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
            BookingDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(5).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(12).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Dinner"
        };

        public static BookingModel ValidBookingModelWithChangedAllFieldsExceptNames = new BookingModel()
        {
            FirstName = "John",
            LastName = "Wick",
            TotalPrice = 3000,
            DepositPaid = true,
            BookingDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(10).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(17).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Supper"
        };

        public static BookingModel BookingModelWithoutFirstName = new BookingModel()
        {
            LastName = "Smith",
            TotalPrice = 2500,
            DepositPaid = true,
            BookingDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(10).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(17).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithoutLastName = new BookingModel()
        {
            FirstName = "James",
            TotalPrice = 2500,
            DepositPaid = true,
            BookingDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(10).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(17).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithoutTotalPrice = new BookingModel()
        {
            FirstName = "James",
            LastName = "Smith",
            DepositPaid = true,
            BookingDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(10).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(17).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithoutDepositPaid = new BookingModel()
        {
            FirstName = "James",
            LastName = "Smith",
            TotalPrice = 2500,
            BookingDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(10).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(17).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithoutCheckIn = new BookingModel()
        {
            FirstName = "James",
            LastName = "Smith",
            TotalPrice = 2500,
            BookingDates = new BookingDates()
            {
                CheckOut = DateTime.Now.AddDays(17).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithoutCheckOut = new BookingModel()
        {
            FirstName = "James",
            LastName = "Smith",
            TotalPrice = 2500,
            BookingDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(10).ToString("yyyy/MM/dd"),
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
            BookingDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(10).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(17).ToString("yyyy/MM/dd")
            },
        };

        public static BookingModel BookingModelWithNotUpdatedCheckin = new BookingModel()
        {
            FirstName = "James",
            LastName = "Wick",
            TotalPrice = 2000,
            DepositPaid = false,
            BookingDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(5).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(17).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
        };

        public static BookingModel BookingModelWithNotUpdatedCheckout = new BookingModel()
        {
            FirstName = "James",
            LastName = "Wick",
            TotalPrice = 2000,
            DepositPaid = false,
            BookingDates = new BookingDates()
            {
                CheckIn = DateTime.Now.AddDays(10).ToString("yyyy/MM/dd"),
                CheckOut = DateTime.Now.AddDays(12).ToString("yyyy/MM/dd")
            },
            AdditionalNeeds = "Launch"
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

        public static BookingModel[] BookingModelsWithMissingBodyElements()
        {
            return new BookingModel[]
            {
                BookingModelWithoutFirstName,
                BookingModelWithoutLastName,
                BookingModelWithoutTotalPrice,
                BookingModelWithoutDepositPaid,
                BookingModelWithoutCheckIn,
                BookingModelWithoutCheckOut,
                BookingModelWithoutAdditionalNeeds
            };
        }

        public static BookingModel[] BookingModelsForPartialUpdate()
        {
            return new BookingModel[]
            {
                BookingModelWithoutFirstName,
                BookingModelWithoutLastName,
                BookingModelWithoutTotalPrice,
                BookingModelWithoutDepositPaid,
                BookingModelWithNotUpdatedCheckin,
                BookingModelWithNotUpdatedCheckout,
                BookingModelWithoutAdditionalNeeds
            };
        }
    }
}
