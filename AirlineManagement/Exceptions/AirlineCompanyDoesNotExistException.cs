using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    internal class AirlineCompanyDoesNotExistException : Exception
    {
        public AirlineCompanyDoesNotExistException()
        {
        }

        public AirlineCompanyDoesNotExistException(string message) : base(message)
        {
        }

        public AirlineCompanyDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AirlineCompanyDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}