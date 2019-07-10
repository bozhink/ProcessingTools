﻿// <copyright file="JatsArticleMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Meta;

namespace ProcessingTools.Services.Models.Meta
{
    using System;
    using ProcessingTools.Common.Attributes;
    using ProcessingTools.Common.Constants.Schema;

    /// <summary>
    /// JATS Article meta model.
    /// </summary>
    public class JatsArticleMetaModel : IArticleMetaModel
    {
        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleIdOfInternalType)]
        public string ArticleId { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleIdOfTypeDoi)]
        public string Doi { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaTitle)]
        public string Title { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaSubTitle)]
        public string SubTitle { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleJournalMetaJournalId)]
        public string JournalId { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleJournalMetaJournalTitle)]
        public string JournalName { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleJournalMetaJournalAbbreviatedTitle)]
        public string JournalAbbreviatedName { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleJournalMetaPublisherName)]
        public string PublisherName { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaArchivalDate)]
        public DateTime? ArchivalDate { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaPublishedOn)]
        public DateTime? PublishedOn { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaAcceptedOn)]
        public DateTime? AcceptedOn { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaReceivedOn)]
        public DateTime? ReceivedOn { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaVolumeSeries)]
        public string VolumeSeries { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaVolume)]
        public string Volume { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaIssue)]
        public string Issue { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaIssuePart)]
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

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaCountsNumberOfPages)]
        public int NumberOfPages { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaCountsNumberOfReferences)]
        public int NumberOfReferences { get; set; }
    }
}
