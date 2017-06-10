namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class UserNotFoundException : ApplicationException
    {
        private string message;

        public UserNotFoundException()
            : base()
        {
            this.message = null;
        }

        public UserNotFoundException(string message)
            : base(message)
        {
            this.message = message;
        }

        public UserNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.message = message;
        }

        public UserNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.message = null;
        }

        public override string Message => this.message ?? "User not found.";

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
