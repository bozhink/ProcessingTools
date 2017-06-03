namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class MaximalNumberOfIterationsExceededException : Exception
    {
        private const string DefaultMessage = "Maximal number of iterations is exceeded";

        public MaximalNumberOfIterationsExceededException()
            : base(message: DefaultMessage)
        {
        }

        public MaximalNumberOfIterationsExceededException(string message)
            : base(message: message)
        {
        }

        public MaximalNumberOfIterationsExceededException(string message, Exception innerException)
            : base(message: message, innerException: innerException)
        {
        }

        public MaximalNumberOfIterationsExceededException(SerializationInfo info, StreamingContext context)
            : base(info: info, context: context)
        {
        }
    }
}
