using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Helpers;
using RestfulBooker.ApiTests.Models;
using RestfulBooker.ApiTests.Services;
using RestSharp;

namespace RestfulBooker.ApiTests.Factories
{
    public class BookingRequestFactory
    {
        private readonly AuthenticationService _authService;

        public BookingRequestFactory(AuthenticationService authService)
        {
            _authService = authService;
        }

        public RestRequest CreateBookingByIdRequest(int bookingId, Method method)
        {
            var request = new RestRequest(Endpoints.GetBookingByIdEndpoint, method);
            request.AddUrlSegment(Endpoints.GetBookingByIdSegment, bookingId);
            request.AddStandardHeaders();
            request.AddAuthorizationHeader(_authService);
            return request;
        }

        public RestRequest CreateUpdateBookingRequest(BookingModel bookingModel, int bookingId, Method method)
        {
            var request = new RestRequest(Endpoints.GetBookingByIdEndpoint, method);
            request.AddUrlSegment(Endpoints.GetBookingByIdSegment, bookingId);
            request.AddStandardHeaders();
            request.AddAuthorizationHeader(_authService);
            request.AddJsonBody(bookingModel);
            return request;
        }

        public RestRequest CreatePostBookingRequest(BookingModel bookingModel)
        {
            var request = new RestRequest(Endpoints.BookingEndpoint, Method.POST);
            request.AddStandardHeaders();
            request.AddJsonBody(bookingModel);
            return request;
        }

        public RestRequest CreateGetBookingIdsRequest()
        {
            var request = new RestRequest(Endpoints.BookingEndpoint, Method.GET);
            request.AddStandardHeaders();
            return request;
        }

        public RestRequest CreateGetBookingByNameRequest(string firstName, string lastName)
        {
            var request = new RestRequest(Endpoints.BookingEndpoint, Method.GET);
            request.AddQueryParameter(Endpoints.GetBookingByFirstNameSegment, firstName);
            request.AddQueryParameter(Endpoints.GetBookingByLastNameSegment, lastName);
            request.AddStandardHeaders();
            return request;
        }

        public RestRequest CreateGetBookingByDatesRequest(string checkin, string checkout)
        {
            var request = new RestRequest(Endpoints.BookingEndpoint, Method.GET);
            request.AddQueryParameter(Endpoints.GetBookingByCheckinSegment, checkin);
            request.AddQueryParameter(Endpoints.GetBookingByCheckoutSegment, checkout);
            request.AddStandardHeaders();
            return request;
        }

        public RestRequest CreateGetBookingByQueryParameterRequest(string parameterName, string parameterValue)
        {
            var request = new RestRequest(Endpoints.BookingEndpoint, Method.GET);
            request.AddQueryParameter(parameterName, parameterValue);
            request.AddStandardHeaders();
            return request;
        }
    }
}
