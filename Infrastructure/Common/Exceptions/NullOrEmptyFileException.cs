namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

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
    }
}
