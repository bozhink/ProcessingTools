// <copyright file="ArticlesIndexViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Articles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Articles Index View Model
    /// </summary>
    public class ArticlesIndexViewModel : GridViewModel<ArticleIndexViewModel>, ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesIndexViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="numberOfItems">The number of grid items.</param>
        /// <param name="numberOfItemsPerPage">The number of items per page.</param>
        /// <param name="currentPage">The value number of the current page.</param>
        /// <param name="items">The grid items.</param>
        public ArticlesIndexViewModel(UserContext userContext, long numberOfItems, long numberOfItemsPerPage, long currentPage, IEnumerable<ArticleIndexViewModel> items)
            : base(numberOfItems, numberOfItemsPerPage, currentPage, items)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Articles")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
