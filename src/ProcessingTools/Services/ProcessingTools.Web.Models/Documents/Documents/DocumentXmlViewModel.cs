// <copyright file="DocumentXmlViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Documents
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Document XML view model.
    /// </summary>
    public class DocumentXmlViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentXmlViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public DocumentXmlViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Edit Document")]
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
        /// Gets or sets the XML content.
        /// </summary>
        public string Content { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
