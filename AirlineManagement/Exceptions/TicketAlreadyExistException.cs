using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    public class TicketAlreadyExistException : Exception
    {
        public TicketAlreadyExistException()
        {
        }

        public TicketAlreadyExistException(string message) : base(message)
        {
        }

        public TicketAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TicketAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}