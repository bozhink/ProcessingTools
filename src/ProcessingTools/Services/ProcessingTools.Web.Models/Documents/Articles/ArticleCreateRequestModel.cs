// <copyright file="ArticleCreateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Articles
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Documents;
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;

    /// <summary>
    /// Article Create Request Model
    /// </summary>
    public class ArticleCreateRequestModel : ProcessingTools.Models.Contracts.IWebModel, IArticleInsertModel
    {
        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfArticleTitle, MinimumLength = ValidationConstants.MinimalLengthOfArticleTitle)]
        public string Title { get; set; }

        /// <inheritdoc/>
        [StringLength(ValidationConstants.MaximalLengthOfArticleSubTitle, MinimumLength = ValidationConstants.MinimalLengthOfArticleSubTitle)]
        public string SubTitle { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        public string JournalId { get; set; }

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
