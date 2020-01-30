using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    internal class CustomerDeleteErrorException : Exception
    {
        public CustomerDeleteErrorException()
        {
        }

        public CustomerDeleteErrorException(string message) : base(message)
        {
        }

        public CustomerDeleteErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerDeleteErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}