namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidOrEmptyFilesException : ApplicationException
    {
        private readonly IEnumerable<string> fileNames;

        public InvalidOrEmptyFilesException(IEnumerable<string> fileNames)
            : base()
        {
            this.fileNames = fileNames;
        }

        public InvalidOrEmptyFilesException(string message, IEnumerable<string> fileNames)
            : base(message)
        {
            this.fileNames = fileNames;
        }

        public InvalidOrEmptyFilesException(string message, Exception innerException, IEnumerable<string> fileNames)
            : base(message, innerException)
        {
            this.fileNames = fileNames;
        }

        public InvalidOrEmptyFilesException(SerializationInfo info, StreamingContext context, IEnumerable<string> fileNames)
            : base(info, context)
        {
            this.fileNames = fileNames;
        }

        public IEnumerable<string> FileNames => this.fileNames;
    }
}
