// <copyright file="ArticleEditViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Articles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Documents;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Article edit view model.
    /// </summary>
    public class ArticleEditViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleEditViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="journals">Journals for select.</param>
        public ArticleEditViewModel(UserContext userContext, IEnumerable<ArticleJournalViewModel> journals)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            this.Journals = journals ?? throw new ArgumentNullException(nameof(journals));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Edit Article")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfId, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfId)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfArticleTitle, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfArticleTitle)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the subtitle.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleSubTitle, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfArticleSubTitle)]
        [Display(Name = "Subtitle")]
        public string SubTitle { get; set; }

        /// <summary>
        /// Gets or sets the Digital Object Identifier (DOI) of the article.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleDoi, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfArticleDoi)]
        [Display(Name = "DOI")]
        public string Doi { get; set; }

        /// <summary>
        /// Gets or sets the journal ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Journal")]
        public string JournalId { get; set; }

        /// <summary>
        /// Gets the list of journals for select.
        /// </summary>
        public IEnumerable<ArticleJournalViewModel> Journals { get; }

        /// <summary>
        /// Gets or sets the published date.
        /// </summary>
        [Display(Name = "Published on")]
        public DateTime? PublishedOn { get; set; }

        /// <summary>
        /// Gets or sets the accepted date.
        /// </summary>
        [Display(Name = "Accepted on")]
        public DateTime? AcceptedOn { get; set; }

        /// <summary>
        /// Gets or sets the received date.
        /// </summary>
        [Display(Name = "Received on")]
        public DateTime? ReceivedOn { get; set; }

        /// <summary>
        /// Gets or sets the volume series.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleVolumeSeries, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfArticleVolumeSeries)]
        [Display(Name = "Volume series")]
        public string VolumeSeries { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleVolume, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfArticleVolume)]
        [Display(Name = "Volume")]
        public string Volume { get; set; }

        /// <summary>
        /// Gets or sets the issue.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleIssue, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfArticleIssue)]
        [Display(Name = "Issue")]
        public string Issue { get; set; }

        /// <summary>
        /// Gets or sets the issue part.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleIssuePart, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfArticleIssuePart)]
        [Display(Name = "Issue part")]
        public string IssuePart { get; set; }

        /// <summary>
        /// Gets or sets the e-location ID.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleELocationId, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfArticleELocationId)]
        [Display(Name = "e-location ID")]
        public string ELocationId { get; set; }

        /// <summary>
        /// Gets or sets the first page.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleFirstPage, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfArticleFirstPage)]
        [Display(Name = "First page")]
        public string FirstPage { get; set; }

        /// <summary>
        /// Gets or sets the last page.
        /// </summary>
        [StringLength(ValidationConstants.MaximalLengthOfArticleLastPage, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfArticleLastPage)]
        [Display(Name = "Last page")]
        public string LastPage { get; set; }

        /// <summary>
        /// Gets or sets the number of pages.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Number of pages")]
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether article is finalized.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Is finalized")]
        public bool IsFinalized { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether article is deployed.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Is deployed")]
        public bool IsDeployed { get; set; }

        /// <summary>
        /// Gets or sets created by.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets created on.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Created on")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets modified by.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Modified by")]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets modified on.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Modified on")]
        public DateTime ModifiedOn { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
