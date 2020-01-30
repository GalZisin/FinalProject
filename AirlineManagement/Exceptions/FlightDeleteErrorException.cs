using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    internal class FlightDeleteErrorException : Exception
    {
        public FlightDeleteErrorException()
        {
        }

        public FlightDeleteErrorException(string message) : base(message)
        {
        }

        public FlightDeleteErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightDeleteErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}