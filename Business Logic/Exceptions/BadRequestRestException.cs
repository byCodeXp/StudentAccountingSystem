using System;
using System.Globalization;

namespace Business_Logic.Exceptions
{
    public class BadRequestRestException : Exception
    {
        public BadRequestRestException() :base() {}
        
        public BadRequestRestException(string message) : base(message) { }
        
        public BadRequestRestException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}