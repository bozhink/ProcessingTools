// <copyright file="IArticleBaseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Articles
{
    /// <summary>
    /// Article base model.
    /// </summary>
    public interface IArticleBaseModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the subtitle.
        /// </summary>
        string SubTitle { get; set; }

        /// <summary>
        /// Gets or sets the journal ID.
        /// </summary>
        string JournalId { get; set; }

        /// <summary>
        /// Gets or sets the volume series.
        /// </summary>
        string VolumeSeries { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        string Volume { get; set; }

        /// <summary>
        /// Gets or sets the issue.
        /// </summary>
        string Issue { get; set; }

        /// <summary>
        /// Gets or sets the issue part.
        /// </summary>
        string IssuePart { get; set; }

        /// <summary>
        /// Gets or sets the e-location ID.
        /// </summary>
        string ELocationId { get; set; }

        /// <summary>
        /// Gets or sets the first page.
        /// </summary>
        string FirstPage { get; set; }

        /// <summary>
        /// Gets or sets the last page.
        /// </summary>
        string LastPage { get; set; }

        /// <summary>
        /// Gets or sets the number of pages.
        /// </summary>
        int NumberOfPages { get; set; }
    }
}
