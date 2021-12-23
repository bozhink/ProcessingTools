// <copyright file="ArticleDeleteViewModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Articles
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Article delete view model.
    /// </summary>
    public class ArticleDeleteViewModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleDeleteViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="journal">Selected journal.</param>
        public ArticleDeleteViewModel(UserContext userContext, ArticleJournalViewModel journal)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            this.Journal = journal ?? throw new ArgumentNullException(nameof(journal));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Delete Article")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the article ID.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Article ID")]
        public string ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the subtitle.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Subtitle")]
        public string SubTitle { get; set; }

        /// <summary>
        /// Gets or sets the Digital Object Identifier (DOI) of the article.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "DOI")]
        public string Doi { get; set; }

        /// <summary>
        /// Gets or sets the journal ID.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Journal")]
        public string JournalId { get; set; }

        /// <summary>
        /// Gets the selected journal.
        /// </summary>
        public ArticleJournalViewModel Journal { get; }

        /// <summary>
        /// Gets or sets the published date.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Published on")]
        public DateTime? PublishedOn { get; set; }

        /// <summary>
        /// Gets or sets the archival date.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Archived on")]
        public DateTime? ArchivedOn { get; set; }

        /// <summary>
        /// Gets or sets the accepted date.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Accepted on")]
        public DateTime? AcceptedOn { get; set; }

        /// <summary>
        /// Gets or sets the received date.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Received on")]
        public DateTime? ReceivedOn { get; set; }

        /// <summary>
        /// Gets or sets the volume series.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Volume series")]
        public string VolumeSeries { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Volume")]
        public string Volume { get; set; }

        /// <summary>
        /// Gets or sets the issue.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Issue")]
        public string Issue { get; set; }

        /// <summary>
        /// Gets or sets the issue part.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Issue part")]
        public string IssuePart { get; set; }

        /// <summary>
        /// Gets or sets the e-location ID.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "e-location ID")]
        public string ELocationId { get; set; }

        /// <summary>
        /// Gets or sets the first page.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "First page")]
        public string FirstPage { get; set; }

        /// <summary>
        /// Gets or sets the last page.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Last page")]
        public string LastPage { get; set; }

        /// <summary>
        /// Gets or sets the number of pages.
        /// </summary>
        [ReadOnly(true)]
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
        public Uri ReturnUrl { get; set; }
    }
}
