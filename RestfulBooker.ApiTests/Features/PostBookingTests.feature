@GetInitialBookingIds
@TestDataCleanup
Feature: Post Bookings endpoint tests

Scenario: Post Booking returns valid Booking when it is created
Given valid bookings models exist
| FirstName | LastName | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Jack      | Mamoa    | 1000       | true        | 2020-08-23 / 2020-08-30 | Breakfasts      |
| Kate      | Winslet  | 1500       | false       | 2020-09-23 / 2020-09-30 | Breakfasts      |
When POST Bookings request with complete object is sent
And GET Booking by Id request is sent
Then expected bookings should be valid to booking responses
And expected bookings should return expected status code 200

Scenario Outline: Post Booking returns valid Booking without not necessary row when it is created
Given valid bookings models without <ExcludedRow> exist
| FirstName | LastName | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Jack      | Mamoa    | 1000       | true        | 2020-08-23 / 2020-08-30 | Breakfasts      |
| Kate      | Winslet  | 1500       | false       | 2020-09-23 / 2020-09-30 | Breakfasts      |
When POST Bookings request with incomplete object is sent
And GET Bookings Ids request is sent
Then expected bookings should exist
And expected bookings should return expected status code 200
Examples: 
| ExcludedRow     |
| AdditionalNeeds |
     
Scenario Outline: Post Booking returns Internal Server Error status code when invalid Booking Model is sent
Given invalid bookings models without <ExcludedRow> exists
| FirstName | LastName | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Jack      | Mamoa    | 1000       | true        | 2020-08-23 / 2020-08-30 | Breakfasts      |
When POST Bookings request with incomplete object is sent
Then expected bookings should return expected status code 500
And bookings should not exist
Examples: 
| ExcludedRow  |
| FirstName    |
| LastName     |
| BookingDates |
| DepositPaid  |
| TotalPrice   |