using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    internal class AirlineCompanyUpdateErrorException : Exception
    {
        public AirlineCompanyUpdateErrorException()
        {
        }

        public AirlineCompanyUpdateErrorException(string message) : base(message)
        {
        }

        public AirlineCompanyUpdateErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AirlineCompanyUpdateErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}