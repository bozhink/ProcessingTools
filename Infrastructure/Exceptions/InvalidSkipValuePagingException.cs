namespace ProcessingTools.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidSkipValuePagingException : ArgumentException
    {
        public InvalidSkipValuePagingException()
            : base()
        {
        }

        public InvalidSkipValuePagingException(string message)
            : base(message)
        {
        }

        public InvalidSkipValuePagingException(string message, string paramName)
            : base(message, paramName)
        {
        }

        public InvalidSkipValuePagingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidSkipValuePagingException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }

        public InvalidSkipValuePagingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}