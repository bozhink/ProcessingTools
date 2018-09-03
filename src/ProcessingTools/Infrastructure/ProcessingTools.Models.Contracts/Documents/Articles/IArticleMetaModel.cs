// <copyright file="IArticleMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Articles
{
    using System;

    /// <summary>
    /// Article meta model.
    /// </summary>
    public interface IArticleMetaModel
    {
        /// <summary>
        /// Gets the ID of the article given by the publisher.
        /// </summary>
        string ArticleId { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the subtitle.
        /// </summary>
        string SubTitle { get; }

        /// <summary>
        /// Gets the Digital Object Identifier (DOI) of the article.
        /// </summary>
        string Doi { get; }

        /// <summary>
        /// Gets the publication date.
        /// </summary>
        DateTime? PublishedOn { get; }

        /// <summary>
        /// Gets the archival date.
        /// </summary>
        DateTime? ArchivedOn { get; }

        /// <summary>
        /// Gets the acceptance date.
        /// </summary>
        DateTime? AcceptedOn { get; }

        /// <summary>
        /// Gets the received date.
        /// </summary>
        DateTime? ReceivedOn { get; }

        /// <summary>
        /// Gets the volume series.
        /// </summary>
        string VolumeSeries { get; }

        /// <summary>
        /// Gets the volume.
        /// </summary>
        string Volume { get; }

        /// <summary>
        /// Gets the issue.
        /// </summary>
        string Issue { get; }

        /// <summary>
        /// Gets the issue part.
        /// </summary>
        string IssuePart { get; }

        /// <summary>
        /// Gets the e-location ID.
        /// </summary>
        string ELocationId { get; }

        /// <summary>
        /// Gets the first page.
        /// </summary>
        string FirstPage { get; }

        /// <summary>
        /// Gets the last page.
        /// </summary>
        string LastPage { get; }
    }
}
