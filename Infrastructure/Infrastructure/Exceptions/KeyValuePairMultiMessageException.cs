namespace ProcessingTools.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;

    public abstract class KeyValuePairMultiMessageException : MultiMessageException<KeyValuePair<string, string>>
    {
        public KeyValuePairMultiMessageException()
        {
        }

        public KeyValuePairMultiMessageException(string message)
            : base(message)
        {
        }

        public KeyValuePairMultiMessageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}