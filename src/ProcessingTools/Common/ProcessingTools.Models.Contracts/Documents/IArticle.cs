// <copyright file="IArticle.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Article.
    /// </summary>
    public interface IArticle : IGuidIdentifiable, IModelWithUserInformation
    {
        /// <summary>
        /// Gets title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets date received.
        /// </summary>
        DateTime? DateReceived { get; }

        /// <summary>
        /// Gets date accepted.
        /// </summary>
        DateTime? DateAccepted { get; }

        /// <summary>
        /// Gets date published.
        /// </summary>
        DateTime? DatePublished { get; }

        /// <summary>
        /// Gets volume.
        /// </summary>
        int? Volume { get; }

        /// <summary>
        /// Gets issue.
        /// </summary>
        int? Issue { get; }

        /// <summary>
        /// Gets first page.
        /// </summary>
        int? FirstPage { get; }

        /// <summary>
        /// Gets last page.
        /// </summary>
        int? LastPage { get; }

        /// <summary>
        /// Gets e-location ID.
        /// </summary>
        string ELocationId { get; }

        /// <summary>
        /// Gets journal ID.
        /// </summary>
        Guid JournalId { get; }

        /// <summary>
        /// Gets documents.
        /// </summary>
        IEnumerable<IDocument> Documents { get; }

        /// <summary>
        /// Gets authors.
        /// </summary>
        IEnumerable<IAuthor> Authors { get; }
    }
}
