// <copyright file="ArticleMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.Meta
{
    using System;
    using ProcessingTools.Attributes;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Harvesters.Models.Contracts.Meta;

    /// <summary>
    /// Article meta model.
    /// </summary>
    public class ArticleMetaModel : IArticleMetaModel
    {
        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleIdOfTypeDoi)]
        public string Doi { get; set; }

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
        [XPath(XPathStrings.ArticleMetaVolume)]
        public string Volume { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaIssue)]
        public string Issue { get; set; }

        /// <inheritdoc/>
        public string IssuePart { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaElocationId)]
        public string ELocationId { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaFirstPage)]
        public string FirstPage { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaLastPage)]
        public string LastPage { get; set; }
    }
}
