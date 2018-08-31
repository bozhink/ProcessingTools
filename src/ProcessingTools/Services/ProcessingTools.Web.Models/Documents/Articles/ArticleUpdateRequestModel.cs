// <copyright file="ArticleUpdateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Articles
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Documents;
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;

    /// <summary>
    /// Article Edit Request Model
    /// </summary>
    public class ArticleUpdateRequestModel : IArticleUpdateModel, ProcessingTools.Models.Contracts.IWebModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfArticleTitle, MinimumLength = ValidationConstants.MinimalLengthOfArticleTitle)]
        public string Title { get; set; }

        /// <inheritdoc/>
        [StringLength(ValidationConstants.MaximalLengthOfArticleSubTitle, MinimumLength = ValidationConstants.MinimalLengthOfArticleSubTitle)]
        public string SubTitle { get; set; }

        /// <inheritdoc/>
        [StringLength(ValidationConstants.MaximalLengthOfArticleDoi, MinimumLength = ValidationConstants.MinimalLengthOfArticleDoi)]
        public string Doi { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string JournalId { get; set; }

        /// <inheritdoc/>
        public DateTime? PublishedOn { get; set; }

        /// <inheritdoc/>
        public DateTime? AcceptedOn { get; set; }

        /// <inheritdoc/>
        public DateTime? ReceivedOn { get; set; }

        /// <inheritdoc/>
        [StringLength(ValidationConstants.MaximalLengthOfArticleVolumeSeries, MinimumLength = ValidationConstants.MinimalLengthOfArticleVolumeSeries)]
        public string VolumeSeries { get; set; }

        /// <inheritdoc/>
        [StringLength(ValidationConstants.MaximalLengthOfArticleVolume, MinimumLength = ValidationConstants.MinimalLengthOfArticleVolume)]
        public string Volume { get; set; }

        /// <inheritdoc/>
        [StringLength(ValidationConstants.MaximalLengthOfArticleIssue, MinimumLength = ValidationConstants.MinimalLengthOfArticleIssue)]
        public string Issue { get; set; }

        /// <inheritdoc/>
        [StringLength(ValidationConstants.MaximalLengthOfArticleIssuePart, MinimumLength = ValidationConstants.MinimalLengthOfArticleIssuePart)]
        public string IssuePart { get; set; }

        /// <inheritdoc/>
        [StringLength(ValidationConstants.MaximalLengthOfArticleELocationId, MinimumLength = ValidationConstants.MinimalLengthOfArticleELocationId)]
        public string ELocationId { get; set; }

        /// <inheritdoc/>
        [StringLength(ValidationConstants.MaximalLengthOfArticleFirstPage, MinimumLength = ValidationConstants.MinimalLengthOfArticleFirstPage)]
        public string FirstPage { get; set; }

        /// <inheritdoc/>
        [StringLength(ValidationConstants.MaximalLengthOfArticleLastPage, MinimumLength = ValidationConstants.MinimalLengthOfArticleLastPage)]
        public string LastPage { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public int NumberOfPages { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
