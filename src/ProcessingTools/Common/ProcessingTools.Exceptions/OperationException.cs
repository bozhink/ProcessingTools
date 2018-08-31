// <copyright file="OperationException.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents error that occur when user is not found.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class OperationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationException"/> class with default
        /// error message.
        /// </summary>
        public OperationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationException"/> class with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OperationException(string message)
            : base(message: message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public OperationException(string message, Exception innerException)
            : base(message: message, innerException: innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public OperationException(SerializationInfo info, StreamingContext context)
            : base(info: info, context: context)
        {
        }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info: info, context: context);
        }
    }
}
