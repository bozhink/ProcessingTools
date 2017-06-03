namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

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
    }
}
