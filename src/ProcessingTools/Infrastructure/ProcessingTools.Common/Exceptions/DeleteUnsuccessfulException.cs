// <copyright file="DeleteUnsuccessfulException.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using ProcessingTools.Common.Resources;

    /// <summary>
    /// Represents error that occur when delete operation is not successful.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Delete this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class DeleteUnsuccessfulException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUnsuccessfulException"/> class with default
        /// error message.
        /// </summary>
        public DeleteUnsuccessfulException()
            : base(message: StringResources.DeleteOperationIsNotSuccessful)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUnsuccessfulException"/> class with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DeleteUnsuccessfulException(string message)
            : base(message: $"{StringResources.DeleteOperationIsNotSuccessful}{Environment.NewLine}{message}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUnsuccessfulException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public DeleteUnsuccessfulException(string message, Exception innerException)
            : base(message: $"{StringResources.DeleteOperationIsNotSuccessful}{Environment.NewLine}{message}", innerException: innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUnsuccessfulException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected DeleteUnsuccessfulException(SerializationInfo info, StreamingContext context)
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
