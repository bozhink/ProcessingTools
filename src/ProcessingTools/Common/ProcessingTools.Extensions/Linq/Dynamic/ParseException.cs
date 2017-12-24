// <copyright file="ParseException.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace System.Linq.Dynamic
{
    /// <summary>
    /// Parse Exception.
    /// </summary>
    public sealed class ParseException : Exception
    {
        private readonly int position;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="position">The position of parsing error.</param>
        public ParseException(string message, int position)
            : base(message)
        {
            this.position = position;
        }

        /// <summary>
        /// Gets position.
        /// </summary>
        public int Position => this.position;

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(Resources.ParseExceptionFormat, this.Message, this.position);
        }
    }
}
