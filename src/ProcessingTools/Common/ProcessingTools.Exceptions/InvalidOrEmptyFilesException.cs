// <copyright file="InvalidOrEmptyFilesException.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents error that occur when invlaid or empty files are provided for processing.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Sonar Code Smell", "S3925: Update this implementation of 'ISerializable' to conform to the recommended serialization pattern", Justification = "Not Applicable")]
    [Serializable]
    public class InvalidOrEmptyFilesException : Exception
    {
        private readonly IEnumerable<string> fileNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOrEmptyFilesException"/> class with default
        /// error message, and a list of the names of all invalid files.
        /// </summary>
        /// <param name="fileNames">List of the names of all invalid files</param>
        public InvalidOrEmptyFilesException(IEnumerable<string> fileNames)
        {
            this.fileNames = fileNames;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOrEmptyFilesException"/> class with a specified
        /// error message, and a list of the names of all invalid files.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="fileNames">List of the names of all invalid files</param>
        public InvalidOrEmptyFilesException(string message, IEnumerable<string> fileNames)
            : base(message: message)
        {
            this.fileNames = fileNames;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOrEmptyFilesException"/> class with a specified
        /// error message, reference to the inner exception that is the cause of this exception, and
        /// a list of the names of all invalid files.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <param name="fileNames">List of the names of all invalid files</param>
        public InvalidOrEmptyFilesException(string message, Exception innerException, IEnumerable<string> fileNames)
            : base(message: message, innerException: innerException)
        {
            this.fileNames = fileNames;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOrEmptyFilesException"/> class with serialized data, and
        /// a list of the names of all invalid files
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// <param name="fileNames">List of the names of all invalid files</param>
        public InvalidOrEmptyFilesException(SerializationInfo info, StreamingContext context, IEnumerable<string> fileNames)
            : base(info: info, context: context)
        {
            this.fileNames = fileNames;
        }

        /// <summary>
        /// Gets the list of the names of all invalid files.
        /// </summary>
        public IEnumerable<string> FileNames => this.fileNames;

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info: info, context: context);
        }
    }
}
