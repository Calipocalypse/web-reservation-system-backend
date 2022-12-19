using System;
using System.Runtime.Serialization;

namespace Wsr.Models.Exceptions
{
    public class GuidParseFailedException : Exception
    {
        public GuidParseFailedException()
        {
        }

        public GuidParseFailedException(string message) : base(message)
        {
        }

        public GuidParseFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GuidParseFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
