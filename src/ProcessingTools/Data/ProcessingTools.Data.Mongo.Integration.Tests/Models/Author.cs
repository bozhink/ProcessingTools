// <copyright file="Author.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Integration.Tests.Models
{
    /// <summary>
    /// Author.
    /// </summary>
    internal class Author
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> class.
        /// </summary>
        public Author()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> class.
        /// </summary>
        /// <param name="firstName">First name of the author.</param>
        /// <param name="lastName">Last name of the author.</param>
        public Author(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        /// <summary>
        /// Gets or sets the first name of the author.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the author.
        /// </summary>
        public string LastName { get; set; }

        /// <inheritdoc/>
        public override string ToString() => $"{this.FirstName} {this.LastName}";
    }
}
