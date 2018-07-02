// <copyright file="ArticleCreateFromFileViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Articles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Article create from file view model.
    /// </summary>
    public class ArticleCreateFromFileViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleCreateFromFileViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="journals">Journals for select.</param>
        public ArticleCreateFromFileViewModel(UserContext userContext, IEnumerable<ArticleJournalViewModel> journals)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            this.Journals = journals ?? throw new ArgumentNullException(nameof(journals));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Create New Article From File")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        [Display(Name = "File")]
        public string File { get; set; }

        /// <summary>
        /// Gets or sets the journal ID.
        /// </summary>
        [Display(Name = "Journal")]
        public string JournalId { get; set; }

        /// <summary>
        /// Gets the list of journals for select.
        /// </summary>
        public IEnumerable<ArticleJournalViewModel> Journals { get; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
