using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulBooker.ApiTests.Constants
{
    public static class Endpoints
    {
        public static string AuthorizationEndpoint => "/auth";

        public static string BookingEndpoint => "/booking";

        public static string GetBookingByIdEndpoint => "/booking/{bookingId}";

        public static string GetBookingByIdSegment => "bookingId";
    }
}
