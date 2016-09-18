using System;

namespace com.mobiquityinc.exception
{
    public sealed class APIException : ApplicationException
    {
        public APIException(string message) : base(message)
        {
        }

        public APIException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
