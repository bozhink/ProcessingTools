// <copyright file="DocumentHtmlViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Documents
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Document HTML view model.
    /// </summary>
    public class DocumentHtmlViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentHtmlViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public DocumentHtmlViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Document Preview")]
        public virtual string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the object ID of the article.
        /// </summary>
        public string ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the HTML content.
        /// </summary>
        public string Content { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
