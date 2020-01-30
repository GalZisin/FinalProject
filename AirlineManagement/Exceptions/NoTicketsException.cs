using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    public class NoTicketsException : Exception
    {
        public NoTicketsException()
        {
        }

        public NoTicketsException(string message) : base(message)
        {
        }

        public NoTicketsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoTicketsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}