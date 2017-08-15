namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class KeyExistsException : Exception
    {
        private const string DefaultMessage = "Key already exists";

        public KeyExistsException()
            : base(message: DefaultMessage)
        {
        }

        public KeyExistsException(string message)
            : base(message: message)
        {
        }

        public KeyExistsException(string message, Exception innerException)
            : base(message: message, innerException: innerException)
        {
        }

        public KeyExistsException(SerializationInfo info, StreamingContext context)
            : base(info: info, context: context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
