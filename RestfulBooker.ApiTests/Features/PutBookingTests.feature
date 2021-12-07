﻿@TestDataCleanup
Feature: Put Bookings endpoint tests

@GetInitialBookingIds
Scenario: Put Booking returns updated Bookings when request with new data is sent
Given bookings exist
| FirstName | LastName | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Jack      | Mamoa    | 1000       | true        | 2020-08-23 / 2020-08-30 | Breakfasts      |
| Kate      | Winslet  | 1500       | false       | 2020-09-23 / 2020-09-30 | Breakfasts      |
When PUT Bookings request with following data is sent
| FirstName | LastName | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Derric    | Green    | 2500       | false       | 2021-10-20 / 2021-10-28 | Parking         |
| Kia       | Madson   | 1750       | true        | 2021-07-17 / 2021-07-29 | Dinner          |
And GET Bookings Ids request is sent
Then expected bookings should exist

Scenario Outline: Put Booking returns Bad Request status code when incompleted Booking Model is sent
Given bookings exist
| FirstName | LastName | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Jack      | Mamoa    | 1000       | true        | 2020-08-23 / 2020-08-30 | Breakfasts      |
| Kate      | Winslet  | 1500       | false       | 2020-09-23 / 2020-09-30 | Breakfasts      |
When PUT Bookings request with invalid data without <excludedRow> is sent
| FirstName | LastName | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Jack      | Mamoa    | 1000       | true        | 2020-08-23 / 2020-08-30 | Breakfasts      |
Then expected bookings should return expected status code 400
And GET Booking by Id request is sent
And bookings should not be updated
Examples: 
| excludedRow  |
| FirstName    |
| LastName     |
| BookingDates |
| TotalPrice   |
| DepositPaid  |