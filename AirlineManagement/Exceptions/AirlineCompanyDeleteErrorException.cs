using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    public class AirlineCompanyDeleteErrorException : Exception
    {
        public AirlineCompanyDeleteErrorException()
        {
        }

        public AirlineCompanyDeleteErrorException(string message) : base(message)
        {
        }

        public AirlineCompanyDeleteErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AirlineCompanyDeleteErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}