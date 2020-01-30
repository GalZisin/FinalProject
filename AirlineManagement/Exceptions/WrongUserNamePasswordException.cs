using System;
using System.Runtime.Serialization;

namespace AirlineManagement
{
    [Serializable]
    public class WrongUserNamePasswordException : Exception
    {
        public WrongUserNamePasswordException()
        {
        }

        public WrongUserNamePasswordException(string message) : base(message)
        {
        }

        public WrongUserNamePasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongUserNamePasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}