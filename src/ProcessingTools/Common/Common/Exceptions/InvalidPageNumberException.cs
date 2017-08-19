namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class InvalidPageNumberException : ArgumentException
    {
        public InvalidPageNumberException()
            : base()
        {
        }

        public InvalidPageNumberException(string message)
            : base(message)
        {
        }

        public InvalidPageNumberException(string message, string paramName)
            : base(message, paramName)
        {
        }

        public InvalidPageNumberException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidPageNumberException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }

        public InvalidPageNumberException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
