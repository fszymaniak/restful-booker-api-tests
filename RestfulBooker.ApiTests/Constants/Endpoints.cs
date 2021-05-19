namespace RestfulBooker.ApiTests.Constants
{
    public static class Endpoints
    {
        public static string AuthorizationEndpoint => "/auth";

        public static string BookingEndpoint => "/booking";

        public static string GetBookingByIdEndpoint => "/booking/{bookingId}";

        public static string GetBookingByIdSegment => "bookingId";

        public static string GetBookingByFirstNameSegment => "firstname";

        public static string GetBookingByLastNameSegment => "lastname";

        public static string GetBookingByCheckinSegment => "checkin";

        public static string GetBookingByCheckoutSegment => "checkout";
    }
}
