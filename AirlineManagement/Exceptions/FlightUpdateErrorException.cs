using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    internal class FlightUpdateErrorException : Exception
    {
        public FlightUpdateErrorException()
        {
        }

        public FlightUpdateErrorException(string message) : base(message)
        {
        }

        public FlightUpdateErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightUpdateErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}