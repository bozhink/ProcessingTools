namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class InvalidUserIdException : ApplicationException
    {
        private string message;
        private object userId;

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
    }
}
