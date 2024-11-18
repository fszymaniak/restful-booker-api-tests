using System.Net;

namespace RestfulBooker.ApiTests.Models
{
    /// <summary>
    /// Wrapper for API responses providing status information and data.
    /// </summary>
    /// <typeparam name="T">The type of data returned in the response.</typeparam>
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }

        public static ApiResponse<T> Success(T data, HttpStatusCode statusCode)
        {
            return new ApiResponse<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessful = true
            };
        }

        public static ApiResponse<T> Failure(HttpStatusCode statusCode, string errorMessage)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                IsSuccessful = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
