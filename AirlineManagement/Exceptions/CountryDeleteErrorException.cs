using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    internal class CountryDeleteErrorException : Exception
    {
        public CountryDeleteErrorException()
        {
        }

        public CountryDeleteErrorException(string message) : base(message)
        {
        }

        public CountryDeleteErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CountryDeleteErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}