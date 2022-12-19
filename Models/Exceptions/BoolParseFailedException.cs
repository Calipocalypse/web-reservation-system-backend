using System;
using System.Runtime.Serialization;

namespace Wsr.Models.Exceptions
{
    public class BoolParseFailedException : Exception
    {
        public BoolParseFailedException()
        {
        }

        public BoolParseFailedException(string message) : base(message)
        {
        }

        public BoolParseFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BoolParseFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
