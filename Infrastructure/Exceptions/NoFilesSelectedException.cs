namespace ProcessingTools.Exceptions
{
    using System;
    using System.Runtime.Serialization;

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
    }
}
