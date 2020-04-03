using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    public class AdministratorAlreadyExistException : Exception
    {
        public AdministratorAlreadyExistException()
        {
        }

        public AdministratorAlreadyExistException(string message) : base(message)
        {
        }

        public AdministratorAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdministratorAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}