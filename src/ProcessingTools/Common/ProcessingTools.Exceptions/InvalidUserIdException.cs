// <copyright file="InvalidUserIdException.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents error that occur when id of user is invalid.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class InvalidUserIdException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserIdException"/> class with default
        /// error message and id of user.
        /// </summary>
        /// <param name="userId">ID of user that is the cause of the current exception</param>
        public InvalidUserIdException(object userId)
            : base(message: $@"User Id ""{userId}"" not found.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserIdException"/> class with a specified
        /// error message and id of user.
        /// </summary>
        /// <param name="userId">ID of user that is the cause of the current exception</param>
        /// <param name="message">The message that describes the error.</param>
        public InvalidUserIdException(object userId, string message)
            : base(message: $@"User Id ""{userId}"" not found.{Environment.NewLine}{message ?? string.Empty}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserIdException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception and id of user.
        /// </summary>
        /// /// <param name="userId">ID of user that is the cause of the current exception</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidUserIdException(object userId, string message, Exception innerException)
            : base(message: $@"User Id ""{userId}"" not found.{Environment.NewLine}{message ?? string.Empty}", innerException: innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserIdException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public InvalidUserIdException(SerializationInfo info, StreamingContext context)
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
