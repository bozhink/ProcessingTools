// <copyright file="Article.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.Meta
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Harvesters.Meta;

    /// <summary>
    /// Article
    /// </summary>
    public class Article : IArticle
    {
        /// <inheritdoc/>
        public ICollection<IAuthor> Authors { get; set; }

        /// <inheritdoc/>
        public string Doi { get; set; }

        /// <inheritdoc/>
        public string FirstPage { get; set; }

        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Issue { get; set; }

        /// <inheritdoc/>
        public string LastPage { get; set; }

        /// <inheritdoc/>
        public string Title { get; set; }

        /// <inheritdoc/>
        public string Volume { get; set; }
    }
}
