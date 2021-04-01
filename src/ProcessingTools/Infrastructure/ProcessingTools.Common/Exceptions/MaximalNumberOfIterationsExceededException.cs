﻿// <copyright file="MaximalNumberOfIterationsExceededException.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using ProcessingTools.Common.Resources;

    /// <summary>
    /// Represents error that occur when the maximal number of iterations is exceeded.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class MaximalNumberOfIterationsExceededException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaximalNumberOfIterationsExceededException"/> class with default
        /// error message.
        /// </summary>
        public MaximalNumberOfIterationsExceededException()
            : base(message: StringResources.MaximalNumberOfIterationsExceeded)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaximalNumberOfIterationsExceededException"/> class with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MaximalNumberOfIterationsExceededException(string message)
            : base(message: $"{StringResources.MaximalNumberOfIterationsExceeded}{Environment.NewLine}{message}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaximalNumberOfIterationsExceededException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MaximalNumberOfIterationsExceededException(string message, Exception innerException)
            : base(message: $"{StringResources.MaximalNumberOfIterationsExceeded}{Environment.NewLine}{message}", innerException: innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaximalNumberOfIterationsExceededException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected MaximalNumberOfIterationsExceededException(SerializationInfo info, StreamingContext context)
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
