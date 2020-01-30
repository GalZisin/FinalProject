using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    internal class AdministratorDeleteErrorException : Exception
    {
        public AdministratorDeleteErrorException()
        {
        }

        public AdministratorDeleteErrorException(string message) : base(message)
        {
        }

        public AdministratorDeleteErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdministratorDeleteErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}