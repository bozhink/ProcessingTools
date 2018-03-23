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
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfArticleTitle, MinimumLength = ValidationConstants.MinimalLengthOfArticleTitle)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the subtitle.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleSubTitle, MinimumLength = ValidationConstants.MinimalLengthOfArticleSubTitle)]
        public string SubTitle { get; set; }

        /// <summary>
        /// Gets or sets the journal ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string JournalId { get; set; }

        /// <summary>
        /// Gets or sets the volume series.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleVolumeSeries, MinimumLength = ValidationConstants.MinimalLengthOfArticleVolumeSeries)]
        public string VolumeSeries { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleVolume, MinimumLength = ValidationConstants.MinimalLengthOfArticleVolume)]
        public string Volume { get; set; }

        /// <summary>
        /// Gets or sets the issue.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleIssue, MinimumLength = ValidationConstants.MinimalLengthOfArticleIssue)]
        public string Issue { get; set; }

        /// <summary>
        /// Gets or sets the issue part.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleIssuePart, MinimumLength = ValidationConstants.MinimalLengthOfArticleIssuePart)]
        public string IssuePart { get; set; }

        /// <summary>
        /// Gets or sets the e-location ID.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleELocationId, MinimumLength = ValidationConstants.MinimalLengthOfArticleELocationId)]
        public string ELocationId { get; set; }

        /// <summary>
        /// Gets or sets the first page.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleFirstPage, MinimumLength = ValidationConstants.MinimalLengthOfArticleFirstPage)]
        public string FirstPage { get; set; }

        /// <summary>
        /// Gets or sets the last page.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleLastPage, MinimumLength = ValidationConstants.MinimalLengthOfArticleLastPage)]
        public string LastPage { get; set; }

        /// <summary>
        /// Gets or sets the number of pages.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public int NumberOfPages { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
