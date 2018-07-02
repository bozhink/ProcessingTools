// <copyright file="NullOrEmptyFileException.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents error that occur when file is null or empty.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class NullOrEmptyFileException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullOrEmptyFileException"/> class with default
        /// error message.
        /// </summary>
        public NullOrEmptyFileException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullOrEmptyFileException"/> class with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NullOrEmptyFileException(string message)
            : base(message: message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullOrEmptyFileException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public NullOrEmptyFileException(string message, Exception innerException)
            : base(message: message, innerException: innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullOrEmptyFileException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public NullOrEmptyFileException(SerializationInfo info, StreamingContext context)
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
