using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Business_Logic.Exceptions
{
    public class NotFoundRestException : Exception
    {
        public string Message { get; }
        public IEnumerable<IdentityError> Errors { get; }
        
        public NotFoundRestException(string message, IEnumerable<IdentityError> errors = null)
        {
            Message = message;
            Errors = errors;
        }
    }
}