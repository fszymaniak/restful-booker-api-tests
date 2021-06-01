Feature: GetBookingIds endpoint tests

@GetInitialBookingIds
Scenario: Get Booking returns valid Booking when Id exists
Given bookings exist
| FirstName | LastName   | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Jack       | Mamoa     | 1000       | true        | 2020-08-23 / 2020-08-30 | Breakfasts      |
| Kate       | Winslet   | 1500       | false       | 2020-09-23 / 2020-09-30 | Breakfasts      |
When GET Bookings Ids request is sent
Then expected bookings should exist

Scenario Outline: Get Bookings Ids filtered by first and/or last name
Given bookings exist
| FirstName | LastName   | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Dirk      | Nowitzki   | 1000       | true        | 2020-07-23 / 2020-07-30 | Breakfasts      |
| Dirk      | Nowitzki   | 2000       | true        | 2020-08-23 / 2020-08-30 | Lunch           |
| Kate      | Winslet    | 1500       | false       | 2020-09-23 / 2020-09-30 | Breakfasts      |
| Evan      | McGregor   | 2500       | false       | 2020-10-23 / 2020-10-30 | Parking         |
When GET filtered Bookings Ids by first and last name: <firstName> <lastName>
Then bookings Ids should be filtered properly
Examples: 
| firstName | lastName |
| Dirk      | Nowitzki |
| Kate      | <null>   |
| <null>    | McGregor |

Scenario Outline: Get Bookings Ids should return empty response when request includes not existing parameters
Given bookings exist
| FirstName | LastName      | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
| Dwyane    | Wade          | 1000       | true        | 2020-07-23 / 2020-07-30 | Breakfasts      |
| Dwyane    | Wade          | 2000       | true        | 2020-08-23 / 2020-08-30 | Lunch           |
| Giannis   | Antetokounmpo | 1500       | false       | 2020-09-23 / 2020-09-30 | Breakfasts      |
| Stephen   | Curry         | 2500       | false       | 2020-10-23 / 2020-10-30 | Parking         |
When GET filtered Bookings Ids by first and last name: <firstName> <lastName>
Then bookings Ids should be filtered properly
Examples: 
| firstName            | lastName            |  
| notExistingFirstName | notExistingLastName |
| notExistingFirstName | <null>              |
| <null>               | notExistingName     |

#@GetInitialBookingIds
#Scenario Outline: Get Bookings Ids filtered by checkin and/or checkout
#Given bookings exist
#| FirstName | LastName   | TotalPrice | DepositPaid | BookingDates            | AdditionalNeeds |
#| Luka      | Doncic     | 1000       | true        | 2020-07-23 / 2020-07-30 | Breakfasts      |
#| Luka      | Doncic     | 2000       | true        | 2020-07-23 / 2020-08-30 | Lunch           |
#| Catelyn   | Parker     | 1500       | false       | 2020-09-23 / 2020-09-30 | Breakfasts      |
#| Adi       | McDonald   | 2500       | false       | 2020-10-23 / 2020-10-30 | Parking         |
#When GET filtered Bookings Ids by checkin and checkout: <checkin> <checkout>
#Then expected bookings should exist
#Examples: 
#| checkin    | checkout   |
#| 2020-07-23 | 2020-07-30 |
#| 2020-07-23 | <null>     |
#| <null>     | 2020-10-30 |
