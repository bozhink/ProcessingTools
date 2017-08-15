namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
