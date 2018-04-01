// <copyright file="ArticleMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.Meta
{
    using System;
    using ProcessingTools.Harvesters.Models.Contracts.Meta;

    /// <summary>
    /// Article meta model.
    /// </summary>
    public class ArticleMetaModel : IArticleMetaModel
    {
        /// <inheritdoc/>
        public string Title { get; set; }

        /// <inheritdoc/>
        public string SubTitle { get; set; }

        /// <inheritdoc/>
        public string JournalId { get; set; }

        /// <inheritdoc/>
        public string JournalName { get; set; }

        /// <inheritdoc/>
        public string JournalAbbreviatedName { get; set; }

        /// <inheritdoc/>
        public string PublisherName { get; set; }

        /// <inheritdoc/>
        public string PublisherAbbreviatedName { get; set; }

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
    }
}
