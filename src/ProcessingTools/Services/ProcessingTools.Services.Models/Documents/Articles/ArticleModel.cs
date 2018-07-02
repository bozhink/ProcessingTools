// <copyright file="ArticleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Articles
{
    using System;
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;

    /// <summary>
    /// Article model.
    /// </summary>
    public class ArticleModel : IArticleModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public bool IsFinalized { get; set; }

        /// <inheritdoc/>
        public bool IsDeployed { get; set; }

        /// <inheritdoc/>
        public string Title { get; set; }

        /// <inheritdoc/>
        public string SubTitle { get; set; }

        /// <inheritdoc/>
        public string Doi { get; set; }

        /// <inheritdoc/>
        public string JournalId { get; set; }

        /// <inheritdoc/>
        public DateTime? PublishedOn { get; set; }

        /// <inheritdoc/>
        public DateTime? AcceptedOn { get; set; }

        /// <inheritdoc/>
        public DateTime? ReceivedOn { get; set; }

        /// <inheritdoc/>
        public string VolumeSeries { get; set; }

        /// <inheritdoc/>
        public string Volume { get; set; }

        /// <inheritdoc/>
        public string Issue { get; set; }

        /// <inheritdoc/>
        public string IssuePart { get; set; }

        /// <inheritdoc/>
        public string ELocationId { get; set; }

        /// <inheritdoc/>
        public string FirstPage { get; set; }

        /// <inheritdoc/>
        public string LastPage { get; set; }

        /// <inheritdoc/>
        public int NumberOfPages { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }
    }
}
