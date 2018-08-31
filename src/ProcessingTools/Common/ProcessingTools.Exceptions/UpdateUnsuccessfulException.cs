// <copyright file="UpdateUnsuccessfulException.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents error that occur when update operation is not successful.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class UpdateUnsuccessfulException : Exception
    {
        private const string DefaultMessage = "Update operation is not successful.";

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUnsuccessfulException"/> class with default
        /// error message.
        /// </summary>
        public UpdateUnsuccessfulException()
            : base(message: DefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUnsuccessfulException"/> class with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UpdateUnsuccessfulException(string message)
            : base(message: $"{DefaultMessage}{Environment.NewLine}{message}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUnsuccessfulException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public UpdateUnsuccessfulException(string message, Exception innerException)
            : base(message: $"{DefaultMessage}{Environment.NewLine}{message}", innerException: innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUnsuccessfulException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public UpdateUnsuccessfulException(SerializationInfo info, StreamingContext context)
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
