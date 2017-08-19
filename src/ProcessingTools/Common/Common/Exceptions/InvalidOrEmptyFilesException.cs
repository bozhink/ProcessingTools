namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class InvalidOrEmptyFilesException : ApplicationException
    {
        private readonly IEnumerable<string> fileNames;

        public InvalidOrEmptyFilesException(IEnumerable<string> fileNames)
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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
