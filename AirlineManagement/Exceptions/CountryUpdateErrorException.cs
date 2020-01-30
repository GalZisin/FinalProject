using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    internal class CountryUpdateErrorException : Exception
    {
        public CountryUpdateErrorException()
        {
        }

        public CountryUpdateErrorException(string message) : base(message)
        {
        }

        public CountryUpdateErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CountryUpdateErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}