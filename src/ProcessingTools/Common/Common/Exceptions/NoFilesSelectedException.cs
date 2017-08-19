namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class NoFilesSelectedException : ApplicationException
    {
        public NoFilesSelectedException()
            : base()
        {
        }

        public NoFilesSelectedException(string message)
            : base(message)
        {
        }

        public NoFilesSelectedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NoFilesSelectedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
