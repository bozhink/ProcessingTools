namespace ProcessingTools.Exceptions
{
    using System;
    using System.Runtime.Serialization;

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
    }
}
