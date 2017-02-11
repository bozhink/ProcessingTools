namespace ProcessingTools.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidItemsPerPageException : ArgumentException
    {
        public InvalidItemsPerPageException()
            : base()
        {
        }

        public InvalidItemsPerPageException(string message)
            : base(message)
        {
        }

        public InvalidItemsPerPageException(string message, string paramName)
            : base(message, paramName)
        {
        }

        public InvalidItemsPerPageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidItemsPerPageException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }

        public InvalidItemsPerPageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
