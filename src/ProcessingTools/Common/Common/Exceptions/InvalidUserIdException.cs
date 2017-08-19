namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class InvalidUserIdException : ApplicationException
    {
        private readonly string message;
        private readonly object userId;

        public InvalidUserIdException(object userId)
            : base()
        {
            this.message = null;
            this.userId = userId;
        }

        public InvalidUserIdException(object userId, string message)
            : base(message)
        {
            this.message = message;
            this.userId = userId;
        }

        public InvalidUserIdException(object userId, string message, Exception innerException)
            : base(message, innerException)
        {
            this.message = message;
            this.userId = userId;
        }

        public InvalidUserIdException(object userId, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.message = null;
            this.userId = userId;
        }

        public override string Message => $"User Id '{this.userId}' not found." + (this.message ?? string.Empty);

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
