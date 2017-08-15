namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class StreamCannotBeReadException : Exception
    {
        private const string DefaultMessage = "Stream can not be read";

        public StreamCannotBeReadException()
            : base(message: DefaultMessage)
        {
        }

        public StreamCannotBeReadException(string message)
            : base(message: message)
        {
        }

        public StreamCannotBeReadException(string message, Exception innerException)
            : base(message: message, innerException: innerException)
        {
        }

        public StreamCannotBeReadException(SerializationInfo info, StreamingContext context)
            : base(info: info, context: context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
