using System;
using System.Globalization;

namespace Business_Logic.Exceptions
{
    public class NotFoundRestException : Exception
    {
        public NotFoundRestException() :base() {}
        
        public NotFoundRestException(string message) : base(message) { }
        
        public NotFoundRestException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}