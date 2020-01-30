using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    public class FlightAlreadyExistException : Exception
    {
        public FlightAlreadyExistException()
        {
        }

        public FlightAlreadyExistException(string message) : base(message)
        {
        }

        public FlightAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlightAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}