namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
