using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    internal class TicketDeleteErrorException : Exception
    {
        public TicketDeleteErrorException()
        {
        }

        public TicketDeleteErrorException(string message) : base(message)
        {
        }

        public TicketDeleteErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TicketDeleteErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}