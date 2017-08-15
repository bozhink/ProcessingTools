namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class FileNameIsNullOrWhitespaceException : Exception
    {
        private const string DefaultMessage = "File name is null or whitespace";

        public FileNameIsNullOrWhitespaceException()
            : base(message: DefaultMessage)
        {
        }

        public FileNameIsNullOrWhitespaceException(string message)
            : base(message: message)
        {
        }

        public FileNameIsNullOrWhitespaceException(string message, Exception innerException)
            : base(message: message, innerException: innerException)
        {
        }

        public FileNameIsNullOrWhitespaceException(SerializationInfo info, StreamingContext context)
            : base(info: info, context: context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
