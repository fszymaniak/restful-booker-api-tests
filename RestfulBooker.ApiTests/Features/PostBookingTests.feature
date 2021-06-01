Feature: Post Bookings endpoint tests

@GetInitialBookingIds
Scenario: Post Booking returns valid Booking when it is created
Given valid bookings models exist
| FirstName | LastName | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Jack      | Mamoa    | 1000       | true        | 2020-08-23 / 2020-08-30 | Breakfasts      |
| Kate      | Winslet  | 1500       | false       | 2020-09-23 / 2020-09-30 | Breakfasts      |
When POST Bookings request is sent
And GET Bookings Ids request is sent
Then expected bookings should exist
And expected bookings should return expected status code 200

@GetInitialBookingIds
Scenario Outline: Post Booking returns valid Booking without not necessary row when it is created
Given valid bookings models without <ExcludedRow> exist
| FirstName | LastName | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Jack      | Mamoa    | 1000       | true        | 2020-08-23 / 2020-08-30 | Breakfasts      |
| Kate      | Winslet  | 1500       | false       | 2020-09-23 / 2020-09-30 | Breakfasts      |
When POST Bookings request is sent
And GET Bookings Ids request is sent
Then expected bookings should exist
And expected bookings should return expected status code 200
Examples: 
| ExcludedRow     |
| AdditionalNeeds |
| DepositPaid     |
| TotalPrice      |     

@GetInitialBookingIds
Scenario Outline: Post Booking returns Internal Server Error status code when invalid Booking Model is sent
Given invalid booking model without <ExcludedRow> exists
| FirstName | LastName | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Jack      | Mamoa    | 1000       | true        | 2020-08-23 / 2020-08-30 | Breakfasts      |
When POST Bookings request is sent
Then expected bookings should return expected status code 500
And bookings should not exist
Examples: 
| ExcludedRow  |
| FirstName    |
| LastName     |
| BookingDates |