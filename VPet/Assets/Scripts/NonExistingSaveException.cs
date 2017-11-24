using System;
using System.Runtime.Serialization;

namespace Assets.Scripts
{
    [Serializable]
    public class NonExistingSaveException : Exception
    {
        public NonExistingSaveException()
        {
        }

        public NonExistingSaveException(string message) : base(message)
        {
        }

        public NonExistingSaveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NonExistingSaveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}