namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class InvalidItemsPerPageException : ArgumentException
    {
        public InvalidItemsPerPageException()
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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
