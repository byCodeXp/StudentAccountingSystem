using System;
using System.Globalization;

namespace Business_Logic.Errors
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException() : base() { }

        public HttpResponseException(string message) : base(message) { }

        public HttpResponseException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
