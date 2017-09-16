// <copyright file="CommandNotFoundException.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents error that occur when invalid command name is provided.
    /// </summary>
    [Serializable]
    public class CommandNotFoundException : Exception
    {
        private const string DefaultMessage = "Command not found.";

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class with
        /// the name of the command that causes this exception.
        /// </summary>
        /// <param name="commandName">The name of the command that caused the current exception.</param>
        public CommandNotFoundException(string commandName)
            : base(message: $"{DefaultMessage} '{commandName}'")
        {
            this.CommandName = commandName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class with a specified
        /// error message and the name of the command that causes this exception.
        /// </summary>
        /// <param name="commandName">The name of the command that caused the current exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public CommandNotFoundException(string commandName, string message)
            : base(message: $"{DefaultMessage} '{commandName}'{Environment.NewLine}{message ?? string.Empty}")
        {
            this.CommandName = commandName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class with a specified
        /// error message, the command name, and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="commandName">The name of the command that caused the current exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CommandNotFoundException(string commandName, string message, Exception innerException)
            : base(message: $"{DefaultMessage} '{commandName}'{Environment.NewLine}{message ?? string.Empty}", innerException: innerException)
        {
            this.CommandName = commandName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public CommandNotFoundException(SerializationInfo info, StreamingContext context)
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
