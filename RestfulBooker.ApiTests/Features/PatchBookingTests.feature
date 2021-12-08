@GetInitialBookingIds
@TestDataCleanup
Feature: Patch Bookings endpoint tests

Scenario Outline: Patch Booking returns valid updated Booking - single row
Given bookings exist
| FirstName | LastName | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Jack      | Mamoa    | 1000       | true        | 2020-08-23 / 2020-08-30 | Breakfasts      |
When PATCH Booking with request from json file 'TestData\Patch\Input\Valid\Patch_valid_input_with_only_<File>.json' is sent
And GET Booking by Id request is sent
Then actual booking is compared with Booking Model from file 'TestData\Patch\Output\Patch_expected_output_with_updated_<File>.json'
And actual bookings should return expected status code 200 
Examples: 
| File            |
| FirstName       |
| LastName        |
| BookingDates    |
| DepositPaid     |
| TotalPrice      |
| AdditionalNeeds |