namespace ProcessingTools.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class MaximalLengthOfFileNameExceededException : Exception
    {
        private const string DefaultMessage = "Maximal length of the file name is exceeded";

        public MaximalLengthOfFileNameExceededException()
            : base(message: DefaultMessage)
        {
        }

        public MaximalLengthOfFileNameExceededException(string message)
            : base(message: message)
        {
        }

        public MaximalLengthOfFileNameExceededException(string message, Exception innerException)
            : base(message: message, innerException: innerException)
        {
        }

        public MaximalLengthOfFileNameExceededException(SerializationInfo info, StreamingContext context)
            : base(info: info, context: context)
        {
        }
    }
}
