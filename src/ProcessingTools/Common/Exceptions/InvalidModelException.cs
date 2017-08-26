// <copyright file="InvalidModelException.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents error that occur when model is invalid.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class InvalidModelException : Exception
    {
        private const string DefaultMessage = "Invalid model.";

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidModelException"/> class with default
        /// error message.
        /// </summary>
        public InvalidModelException()
            : base(DefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidModelException"/> class with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidModelException(string message)
            : base($"{DefaultMessage}{Environment.NewLine}{message}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidModelException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidModelException(string message, Exception innerException)
            : base($"{DefaultMessage}{Environment.NewLine}{message}", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidModelException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public InvalidModelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
