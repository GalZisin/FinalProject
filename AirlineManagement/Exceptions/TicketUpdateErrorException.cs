using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    internal class TicketUpdateErrorException : Exception
    {
        public TicketUpdateErrorException()
        {
        }

        public TicketUpdateErrorException(string message) : base(message)
        {
        }

        public TicketUpdateErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TicketUpdateErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}