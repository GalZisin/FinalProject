using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    public class NoFlightException : Exception
    {
        public NoFlightException()
        {
        }

        public NoFlightException(string message) : base(message)
        {
        }

        public NoFlightException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoFlightException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}