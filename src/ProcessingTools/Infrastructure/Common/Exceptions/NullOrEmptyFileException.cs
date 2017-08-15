namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class NullOrEmptyFileException : ApplicationException
    {
        public NullOrEmptyFileException()
            : base()
        {
        }

        public NullOrEmptyFileException(string message)
            : base(message)
        {
        }

        public NullOrEmptyFileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NullOrEmptyFileException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
