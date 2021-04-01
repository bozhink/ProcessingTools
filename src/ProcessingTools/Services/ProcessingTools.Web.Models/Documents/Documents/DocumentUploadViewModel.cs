// <copyright file="DocumentUploadViewModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Documents
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Document upload view model.
    /// </summary>
    public class DocumentUploadViewModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentUploadViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="article">Article of the document.</param>
        public DocumentUploadViewModel(UserContext userContext, DocumentArticleViewModel article)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            this.Article = article ?? throw new ArgumentNullException(nameof(article));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Upload document")]
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
        /// Gets or sets the object ID of the article.
        /// </summary>
        [Display(Name = "Article")]
        public string ArticleId { get; set; }

        /// <summary>
        /// Gets the article.
        /// </summary>
        public DocumentArticleViewModel Article { get; }

        /// <inheritdoc/>
        public Uri ReturnUrl { get; set; }
    }
}
