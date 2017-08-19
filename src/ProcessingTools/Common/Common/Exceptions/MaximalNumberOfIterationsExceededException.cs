namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
