namespace ProcessingTools.Exceptions
{
    using System;
    using System.Runtime.Serialization;

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
    }
}
