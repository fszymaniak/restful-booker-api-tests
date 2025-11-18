using RestfulBooker.ApiTests.Models;
using System;

namespace RestfulBooker.ApiTests.Builders
{
    public class BookingModelBuilder
    {
        private string _firstName = "John";
        private string _lastName = "Doe";
        private int _totalPrice = 1000;
        private bool _depositPaid = true;
        private string _checkIn = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        private string _checkOut = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
        private string _additionalNeeds = "Breakfast";

        public BookingModelBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public BookingModelBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public BookingModelBuilder WithTotalPrice(int totalPrice)
        {
            _totalPrice = totalPrice;
            return this;
        }

        public BookingModelBuilder WithDepositPaid(bool depositPaid)
        {
            _depositPaid = depositPaid;
            return this;
        }

        public BookingModelBuilder WithCheckInDate(string checkIn)
        {
            _checkIn = checkIn;
            return this;
        }

        public BookingModelBuilder WithCheckOutDate(string checkOut)
        {
            _checkOut = checkOut;
            return this;
        }

        public BookingModelBuilder WithAdditionalNeeds(string additionalNeeds)
        {
            _additionalNeeds = additionalNeeds;
            return this;
        }

        public BookingModelBuilder WithoutFirstName()
        {
            _firstName = null;
            return this;
        }

        public BookingModelBuilder WithoutLastName()
        {
            _lastName = null;
            return this;
        }

        public BookingModelBuilder WithoutAdditionalNeeds()
        {
            _additionalNeeds = null;
            return this;
        }

        public BookingModel Build()
        {
            return new BookingModel
            {
                FirstName = _firstName,
                LastName = _lastName,
                TotalPrice = _totalPrice,
                DepositPaid = _depositPaid,
                BookingDates = new BookingDates
                {
                    CheckIn = _checkIn,
                    CheckOut = _checkOut
                },
                AdditionalNeeds = _additionalNeeds
            };
        }
    }
}
