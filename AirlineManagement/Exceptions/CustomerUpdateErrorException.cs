using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    public class CustomerUpdateErrorException : Exception
    {
        public CustomerUpdateErrorException()
        {
        }

        public CustomerUpdateErrorException(string message) : base(message)
        {
        }

        public CustomerUpdateErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerUpdateErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}