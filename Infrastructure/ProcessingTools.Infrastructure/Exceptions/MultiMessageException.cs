namespace ProcessingTools.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;

    public abstract class MultiMessageException<T> : Exception
    {
        public MultiMessageException()
            : base()
        {
        }

        public MultiMessageException(string message)
            : base(message)
        {
        }

        public MultiMessageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ICollection<T> Messages { get; set; }
    }
}
