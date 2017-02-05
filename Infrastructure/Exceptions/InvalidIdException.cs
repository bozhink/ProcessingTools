namespace ProcessingTools.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidIdException : ArgumentException
    {
        public InvalidIdException()
            : base()
        {
        }

        public InvalidIdException(string message)
            : base(message)
        {
        }

        public InvalidIdException(string message, string paramName)
            : base(message, paramName)
        {
        }

        public InvalidIdException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidIdException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }

        public InvalidIdException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
