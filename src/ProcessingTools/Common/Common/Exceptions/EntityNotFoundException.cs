namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class EntityNotFoundException : ApplicationException
    {
        private readonly string message;

        public EntityNotFoundException()
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
