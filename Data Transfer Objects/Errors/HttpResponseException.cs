using System;
using System.Globalization;

namespace Data_Transfer_Objects.Errors
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
