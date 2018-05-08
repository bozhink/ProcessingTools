// <copyright file="IArticleMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.Contracts.Meta
{
    using System;

    /// <summary>
    /// Article meta model.
    /// </summary>
    public interface IArticleMetaModel
    {
        /// <summary>
        /// Gets article ID given by publisher.
        /// </summary>
        string ArticleId { get; }

        /// <summary>
        /// Gets DOI.
        /// </summary>
        string Doi { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the subtitle.
        /// </summary>
        string SubTitle { get; }

        /// <summary>
        /// Gets the journal ID.
        /// </summary>
        string JournalId { get; }

        /// <summary>
        /// Gets the journal name.
        /// </summary>
        string JournalName { get; }

        /// <summary>
        /// Gets the journal abbreviated name.
        /// </summary>
        string JournalAbbreviatedName { get; }

        /// <summary>
        /// Gets the publisher name.
        /// </summary>
        string PublisherName { get; }

        /// <summary>
        /// Gets archival date.
        /// </summary>
        DateTime? ArchivalDate { get; }

        /// <summary>
        /// Gets the published date.
        /// </summary>
        DateTime? PublishedOn { get; }

        /// <summary>
        /// Gets the accepted date.
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

        /// <summary>
        /// Gets the number of pages.
        /// </summary>
        int NumberOfPages { get; }

        /// <summary>
        /// Gets the number of references.
        /// </summary>
        int NumberOfReferences { get; }
    }
}
