using System;
using System.Globalization;
using System.Net;

namespace Data_Transfer_Objects.Errors
{
    public class HttpResponseException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public HttpResponseException() : base() { }

        public HttpResponseException(string message) : base(message) { }

        public HttpResponseException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
