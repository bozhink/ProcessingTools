﻿// <copyright file="Article.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Meta;

namespace ProcessingTools.Services.Models.Meta
{
    using System.Collections.Generic;

    /// <summary>
    /// Article.
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
