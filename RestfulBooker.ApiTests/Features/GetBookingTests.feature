Feature: GetBooking endpoint tests

Scenario: Get Booking returns valid Booking when Id exists
Given bookings exist
| FirstName | LastName   | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Phill     | Collins    | 1000       | true        | 2020-08-23 / 2020-08-30 | Breakfasts      |
| Lebron    | James      | 1500       | false       | 2020-09-23 / 2020-09-30 | Breakfasts      |
When GET Booking by Id request is sent
Then expected bookings should be valid to booking responses

Scenario: Get Booking returns status code 200 (OK) when Id exists
Given bookings exist
| FirstName | LastName   | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Phill     | Collins    | 1000       | true        | 2020-10-01 / 2020-10-30 | Breakfasts      |
| Lebron    | James      | 1500       | false       | 2020-11-23 / 2020-12-06 | Dinners         |
When GET Booking by Id request returns booking response
Then expected bookings should return expected status code 200
