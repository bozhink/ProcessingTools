﻿// <copyright file="AmbiguousCommandException.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using ProcessingTools.Common.Resources;

    /// <summary>
    /// Represents error that occur when ambiguous command name is provided.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class AmbiguousCommandException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AmbiguousCommandException"/> class with
        /// the name of the command that causes this exception.
        /// </summary>
        /// <param name="commandName">The name of the command that caused the current exception.</param>
        public AmbiguousCommandException(string commandName)
            : base(message: $"{StringResources.AmbiguousCommand} '{commandName}'")
        {
            this.CommandName = commandName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AmbiguousCommandException"/> class with
        /// the name of the command that causes this exception.
        /// </summary>
        /// <param name="commandName">The name of the command that caused the current exception.</param>
        /// <param name="matchedNames">All the commands that match provided command name.</param>
        public AmbiguousCommandException(string commandName, string[] matchedNames)
            : base(message: $"{StringResources.AmbiguousCommand} '{commandName}'{Environment.NewLine}{StringResources.PossibleMatches}: {string.Join(", ", matchedNames)}")
        {
            this.CommandName = commandName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AmbiguousCommandException"/> class with a specified
        /// error message and the name of the command that causes this exception.
        /// </summary>
        /// <param name="commandName">The name of the command that caused the current exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public AmbiguousCommandException(string commandName, string message)
            : base(message: $"{StringResources.AmbiguousCommand} '{commandName}'{Environment.NewLine}{message ?? string.Empty}")
        {
            this.CommandName = commandName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AmbiguousCommandException"/> class with a specified
        /// error message and the name of the command that causes this exception.
        /// </summary>
        /// <param name="commandName">The name of the command that caused the current exception.</param>
        /// <param name="matchedNames">All the commands that match provided command name.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public AmbiguousCommandException(string commandName, string[] matchedNames, string message)
            : base(message: $"{StringResources.AmbiguousCommand} '{commandName}'{Environment.NewLine}{StringResources.PossibleMatches}: {string.Join(", ", matchedNames)}{Environment.NewLine}{message ?? string.Empty}")
        {
            this.CommandName = commandName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AmbiguousCommandException"/> class with a specified
        /// error message, the command name, and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="commandName">The name of the command that caused the current exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public AmbiguousCommandException(string commandName, string message, Exception innerException)
            : base(message: $"{StringResources.AmbiguousCommand} '{commandName}'{Environment.NewLine}{message ?? string.Empty}", innerException: innerException)
        {
            this.CommandName = commandName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AmbiguousCommandException"/> class with a specified
        /// error message, the command name, and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="commandName">The name of the command that caused the current exception.</param>
        /// <param name="matchedNames">All the commands that match provided command name.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public AmbiguousCommandException(string commandName, string[] matchedNames, string message, Exception innerException)
            : base(message: $"{StringResources.AmbiguousCommand} '{commandName}'{Environment.NewLine}{StringResources.PossibleMatches}: {string.Join(", ", matchedNames)}{Environment.NewLine}{message ?? string.Empty}", innerException: innerException)
        {
            this.CommandName = commandName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AmbiguousCommandException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected AmbiguousCommandException(SerializationInfo info, StreamingContext context)
            : base(info: info, context: context)
        {
        }

        /// <summary>
        /// Gets the name of the command that causes this exception.
        /// </summary>
        public string CommandName { get; }

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info: info, context: context);
        }
    }
}
