using System;
using System.Runtime.Serialization;

namespace Wsr.Models.Exceptions
{
    public class DateParseFailedException : Exception
    {
        public DateParseFailedException()
        {
        }

        public DateParseFailedException(string message) : base(message)
        {
        }

        public DateParseFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DateParseFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
