namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class EntityNotFoundException : ApplicationException
    {
        private string message;

        public EntityNotFoundException()
            : base()
        {
            this.message = null;
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
            this.message = message;
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.message = message;
        }

        public EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.message = null;
        }

        public override string Message => this.message ?? "Entity not found.";

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
