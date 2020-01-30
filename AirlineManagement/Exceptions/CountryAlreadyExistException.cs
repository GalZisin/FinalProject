using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    public class CountryAlreadyExistException : Exception
    {
        public CountryAlreadyExistException()
        {
        }

        public CountryAlreadyExistException(string message) : base(message)
        {
        }

        public CountryAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CountryAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}