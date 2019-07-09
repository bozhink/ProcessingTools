// <copyright file="Author.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Meta
{
    using ProcessingTools.Services.Models.Contracts.Meta;

    /// <summary>
    /// Author.
    /// </summary>
    internal class Author : IAuthor
    {
        /// <inheritdoc/>
        public string GivenNames { get; set; }

        /// <inheritdoc/>
        public string Prefix { get; set; }

        /// <inheritdoc/>
        public string Suffix { get; set; }

        /// <inheritdoc/>
        public string Surname { get; set; }
    }
}
