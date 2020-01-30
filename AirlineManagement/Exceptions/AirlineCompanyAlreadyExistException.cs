using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    public class AirlineCompanyAlreadyExistException : Exception
    {
        public AirlineCompanyAlreadyExistException()
        {
        }

        public AirlineCompanyAlreadyExistException(string message) : base(message)
        {
        }

        public AirlineCompanyAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AirlineCompanyAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}