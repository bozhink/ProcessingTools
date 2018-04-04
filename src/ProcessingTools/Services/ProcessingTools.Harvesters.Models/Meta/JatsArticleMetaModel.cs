// <copyright file="JatsArticleMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.Meta
{
    using System;
    using ProcessingTools.Attributes;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Harvesters.Models.Contracts.Meta;

    /// <summary>
    /// JATS Article meta model.
    /// </summary>
    public class JatsArticleMetaModel : IArticleMetaModel
    {
        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleIdOfTypeDoi)]
        public string Doi { get; set; }

        /// <inheritdoc/>
        [XPath(@".//front/article-meta/title-group/article-title")]
        public string Title { get; set; }

        /// <inheritdoc/>
        [XPath(@".//front/article-meta/title-group/subtitle")]
        public string SubTitle { get; set; }

        /// <inheritdoc/>
        [XPath(@".//front/journal-meta/journal-id[@journal-id-type='publisher-id']")]
        public string JournalId { get; set; }

        /// <inheritdoc/>
        [XPath(@".//front/journal-meta/journal-title-group/journal-title")]
        public string JournalName { get; set; }

        /// <inheritdoc/>
        [XPath(@".//front/journal-meta/journal-title-group/abbrev-journal-title")]
        public string JournalAbbreviatedName { get; set; }

        /// <inheritdoc/>
        [XPath(@".//front/journal-meta/publisher/publisher-name")]
        public string PublisherName { get; set; }

        /// <inheritdoc/>
        public string PublisherAbbreviatedName { get; set; }

        /// <inheritdoc/>
        [XPath(@".//front/article-meta/pub-date[@pub-type='epub']")]
        public DateTime? PublishedOn { get; set; }

        /// <inheritdoc/>
        [XPath(@".//front/article-meta/history/date[@date-type=='accepted']")]
        public DateTime? AcceptedOn { get; set; }

        /// <inheritdoc/>
        [XPath(@".//front/article-meta/history/date[@date-type=='received']")]
        public DateTime? ReceivedOn { get; set; }

        /// <inheritdoc/>
        [XPath(@".//front/article-meta/volume-series")]
        public string VolumeSeries { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaVolume)]
        public string Volume { get; set; }

        /// <inheritdoc/>
        [XPath(XPathStrings.ArticleMetaIssue)]
        public string Issue { get; set; }

        /// <inheritdoc/>
        [XPath(@".//front/article-meta/issue-part")]
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
