// <copyright file="InvalidCommandException.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents error that occur when invalid command in current executing context is provided.
    /// </summary>
    [Serializable]
    public class InvalidCommandException : Exception
    {
        private const string DefaultMessage = "Invalid command.";

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandException"/> class with default
        /// error message.
        /// </summary>
        public InvalidCommandException()
            : base(message: DefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandException"/> class with a specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidCommandException(string message)
            : base(message: message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandException"/> class with a specified
        /// error message and the name of the command that causes this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="commandName">The name of the command that caused the current exception.</param>
        public InvalidCommandException(string message, string commandName)
            : base(message: $"{message}{Environment.NewLine}{DefaultMessage} '{commandName}'")
        {
            this.CommandName = commandName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidCommandException(string message, Exception innerException)
            : base(message: message, innerException: innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandException"/> class with a specified
        /// error message, the command name, and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="commandName">The name of the command that caused the current exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidCommandException(string message, string commandName, Exception innerException)
            : base(message: $"{message}{Environment.NewLine}{DefaultMessage} '{commandName}'", innerException: innerException)
        {
            this.CommandName = commandName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCommandException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public InvalidCommandException(SerializationInfo info, StreamingContext context)
            : base(info: info, context: context)
        {
        }

        /// <summary>
        /// Gets the name of the command that causes this exception.
        /// </summary>
        public string CommandName { get; private set; }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info: info, context: context);
        }
    }
}
