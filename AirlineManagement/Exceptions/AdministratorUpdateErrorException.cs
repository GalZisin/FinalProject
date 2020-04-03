using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    public class AdministratorUpdateErrorException : Exception
    {
        public AdministratorUpdateErrorException()
        {
        }

        public AdministratorUpdateErrorException(string message) : base(message)
        {
        }

        public AdministratorUpdateErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdministratorUpdateErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}