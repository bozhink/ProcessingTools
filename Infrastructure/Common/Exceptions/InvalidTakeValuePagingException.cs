namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidTakeValuePagingException : ArgumentException
    {
        public InvalidTakeValuePagingException()
            : base()
        {
        }

        public InvalidTakeValuePagingException(string message)
            : base(message)
        {
        }

        public InvalidTakeValuePagingException(string message, string paramName)
            : base(message, paramName)
        {
        }

        public InvalidTakeValuePagingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidTakeValuePagingException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }

        public InvalidTakeValuePagingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}