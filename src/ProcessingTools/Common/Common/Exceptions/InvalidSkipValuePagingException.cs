namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}