using System.Collections.Generic;
using System.Linq;
using RestfulBooker.ApiTests.Constants;
using RestfulBooker.ApiTests.Models;
using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;
using RestfulBooker.ApiTests.Extensions;
using RestfulBooker.ApiTests.Models.Responses;
using System.Text.RegularExpressions;

namespace RestfulBooker.ApiTests
{
    public abstract class BookingTestBase
    {
        private static readonly IDictionary<string, Method> PostBookingEndpointDictionary = new Dictionary<string, Method>() { { Endpoints.BookingEndpoint, Method.POST } };
        private static readonly IDictionary<string, Method> GetBookingEndpointDictionary = new Dictionary<string, Method>() { { Endpoints.BookingEndpoint, Method.GET } };
        private readonly IDictionary<string, Method> _getBookingByIdEndpointDictionary = new Dictionary<string, Method>() { { Endpoints.GetBookingByIdEndpoint, Method.GET } };

        protected RestClient _client = RestClientExtension.CreateRestClient();

        private readonly RestRequest _requestPost = RestRequestExtension.Create(PostBookingEndpointDictionary);

        private readonly RestRequest _requestGetBooking = RestRequestExtension.Create(GetBookingEndpointDictionary);

        private RestRequest _request = new RestRequest();

        //public async Task<BookingResponse> CreateBooking(string firstName, string lastName, int totalPrice, bool depositPaid, string checkIn, string checkOut, string additionalNeeds)
        //{
        //    var bookingDates = new BookingDates
        //    {
        //        CheckIn = checkIn,
        //        CheckOut = checkOut
        //    };

        //    var bookingRequest = new BookingModel
        //    {
        //        FirstName = firstName,
        //        LastName = lastName,
        //        TotalPrice = totalPrice,
        //        DepositPaid = depositPaid,
        //        BookingDates = bookingDates,
        //        AdditionalNeeds = additionalNeeds
        //    };

        //    _requestPost.PostBookingRequest(bookingRequest);

        //    var response = await _client.ExecuteAsync<BookingResponse>(_requestPost);
        //    var result = JsonSerializer.Deserialize<BookingResponse>(response.Content);

        //    return result;
        //}

        public async Task<IEnumerable<BookingResponse>> CreateBookings(IEnumerable<BookingModel> bookingModels)
        {
            IList<BookingResponse> bookingResponses = new List<BookingResponse>();
            int iterator = 0;

            foreach (var booking in bookingModels)
            {
                _requestPost.RemoveBodyParameter(iterator, index: 2);
                _requestPost.PostBookingRequest(booking);
                var response = await _client.ExecuteAsync<BookingResponse>(_requestPost);
                var result = JsonSerializer.Deserialize<BookingResponse>(response.Content);
                bookingResponses.Add(result);
                iterator++;
            }

            return bookingResponses;
        }

        public async Task<BookingModel> GetBookingById(int bookingId)
        {
            _request = RestRequestExtension.Create(_getBookingByIdEndpointDictionary);
            _request.BookingByIdRequest(bookingId, Method.GET);

            var response = await _client.ExecuteAsync<BookingResponse>(_request);
            var result = JsonSerializer.Deserialize<BookingModel>(response.Content);

            return result;
        }

        public async Task<BookingModel> UpdateBookingById(BookingModel bookingRequest, int bookingId, Method method)
        {
            _request.UpdateBookingByIdRequest(bookingRequest, bookingId, method);

            var response = await _client.ExecuteAsync<BookingResponse>(_request);
            var result = JsonSerializer.Deserialize<BookingModel>(response.Content);

            return result;
        }

        public async Task DeleteBookingsByIds(RestRequest request, IEnumerable<int> bookingIds)
        {
            request.AddAuthorizationHeader();

            foreach(var id in bookingIds)
            {
                
                request.BookingByIdRequest(id, Method.DELETE);

                await _client.ExecuteAsync<HttpResponse>(request);

                request.Parameters.RemoveAt(4);
            }
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIds()
        {
            return await _client.RestResponseAsync<BookingIdsResponse, BookingIdsResponse>(_requestGetBooking);
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByQueryFilters(string filter1, string filter2)
        {
            

            string pattern = @"^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])$";
            var regex = new Regex(pattern);
            if ((regex.IsMatch(filter1) && regex.IsMatch(filter2)) || (regex.IsMatch(filter1) ^ regex.IsMatch(filter2)))
            { 
                _requestGetBooking.GetBookingByCheckinAndCheckoutRequest(filter1, filter2);
            }
            else
            {
                _requestGetBooking.GetBookingByFirstAndLastNameRequest(filter1, filter2);

            }
            var response = await _client.ExecuteAsync<BookingIdsResponse>(_requestGetBooking);
            var result = JsonSerializer.Deserialize<IEnumerable<BookingIdsResponse>>(response.Content);

            return result;
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByCheckinAndCheckout(string checkin, string checkout)
        {
            //_request.GetBookingByCheckinAndCheckoutRequest(checkin, checkout);

            var response = await _client.ExecuteAsync<BookingIdsResponse>(_request);
            var result = JsonSerializer.Deserialize<IEnumerable<BookingIdsResponse>>(response.Content);

            return result;
        }

        public async Task<IEnumerable<BookingIdsResponse>> GetBookingIdsByQueryParameter(string parameterName, string parameterValue)
        {
            _request.GetBookingByQueryParameterRequest(parameterName, parameterValue);

            var response = await _client.ExecuteAsync<BookingIdsResponse>(_request);
            var result = JsonSerializer.Deserialize<IEnumerable<BookingIdsResponse>>(response.Content);

            return result;
        }

        
    }
}
