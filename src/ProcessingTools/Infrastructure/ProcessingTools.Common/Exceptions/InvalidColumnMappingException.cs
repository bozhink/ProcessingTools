﻿// <copyright file="InvalidColumnMappingException.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Invalid column mapping exception.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class InvalidColumnMappingException : InvalidOperationException
    {
        private const string DefaultMessage = "Invalid column mapping.";

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidColumnMappingException"/> class with default
        /// error message.
        /// </summary>
        public InvalidColumnMappingException()
            : base(message: DefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidColumnMappingException"/> class with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidColumnMappingException(string message)
            : base(message: $"{DefaultMessage}{Environment.NewLine}{message}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidColumnMappingException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidColumnMappingException(string message, Exception innerException)
            : base(message: $"{DefaultMessage}{Environment.NewLine}{message}", innerException: innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidColumnMappingException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected InvalidColumnMappingException(SerializationInfo info, StreamingContext context)
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