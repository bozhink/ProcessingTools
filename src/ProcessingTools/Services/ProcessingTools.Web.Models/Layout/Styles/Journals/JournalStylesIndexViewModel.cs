// <copyright file="JournalStylesIndexViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Journals
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Journal styles index view model.
    /// </summary>
    public class JournalStylesIndexViewModel : GridViewModel<JournalStyleIndexViewModel>, ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalStylesIndexViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="numberOfItems">The number of grid items.</param>
        /// <param name="numberOfItemsPerPage">The number of items per page.</param>
        /// <param name="currentPage">The value number of the current page.</param>
        /// <param name="items">The grid items.</param>
        public JournalStylesIndexViewModel(UserContext userContext, long numberOfItems, long numberOfItemsPerPage, long currentPage, IEnumerable<JournalStyleIndexViewModel> items)
            : base(numberOfItems, numberOfItemsPerPage, currentPage, items)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Journal Styles")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
