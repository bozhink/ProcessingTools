namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class KeyNotFoundException : Exception
    {
        private const string DefaultMessage = "Key not found";

        public KeyNotFoundException()
            : base(message: DefaultMessage)
        {
        }

        public KeyNotFoundException(string message)
            : base(message: message)
        {
        }

        public KeyNotFoundException(string message, Exception innerException)
            : base(message: message, innerException: innerException)
        {
        }

        public KeyNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info: info, context: context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
