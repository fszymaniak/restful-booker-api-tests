using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulBooker.ApiTests.Constants
{
    public static class HttpHeaders
    {
        public static class Name
        {
            public static string ContentType => "Content-Type";

            public static string Accept => "Accept";

            public static string Cookie => "Cookie";

            public static string Authorization => "Authorization";
        }

        public static class Value
        {
            public static string ApplicationJson => "application/json";

            public static string AuthorizationBasic => "Basic";
        }
        
    }
}
