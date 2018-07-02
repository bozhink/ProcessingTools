// <copyright file="IArticle.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.Contracts.Meta
{
    using System.Collections.Generic;

    /// <summary>
    /// Article.
    /// </summary>
    public interface IArticle
    {
        /// <summary>
        /// Gets ID.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets DOI.
        /// </summary>
        string Doi { get; }

        /// <summary>
        /// Gets title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets volume.
        /// </summary>
        string Volume { get; }

        /// <summary>
        /// Gets issue.
        /// </summary>
        string Issue { get; }

        /// <summary>
        /// Gets first page.
        /// </summary>
        string FirstPage { get; }

        /// <summary>
        /// Gets last page.
        /// </summary>
        string LastPage { get; }

        /// <summary>
        /// Gets authors.
        /// </summary>
        ICollection<IAuthor> Authors { get; }
    }
}
