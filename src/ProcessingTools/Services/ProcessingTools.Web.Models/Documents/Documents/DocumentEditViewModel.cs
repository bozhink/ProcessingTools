﻿// <copyright file="DocumentEditViewModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Documents
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Document edit view model.
    /// </summary>
    public class DocumentEditViewModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentEditViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="article">Article of the document.</param>
        /// <param name="file">File of the document.</param>
        public DocumentEditViewModel(UserContext userContext, DocumentArticleViewModel article, DocumentFileViewModel file)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            this.Article = article ?? throw new ArgumentNullException(nameof(article));
            this.File = file ?? throw new ArgumentNullException(nameof(file));
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
        [ReadOnly(true)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the object ID of the article.
        /// </summary>
        [ReadOnly(true)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Article")]
        public string ArticleId { get; set; }

        /// <summary>
        /// Gets the article of the document.
        /// </summary>
        public DocumentArticleViewModel Article { get; }

        /// <summary>
        /// Gets or sets the object ID of the file.
        /// </summary>
        [ReadOnly(true)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "File")]
        public string FileId { get; set; }

        /// <summary>
        /// Gets the file of the document.
        /// </summary>
        public DocumentFileViewModel File { get; }

        /// <summary>
        /// Gets or sets a value indicating whether document is final.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Is final")]
        public bool IsFinal { get; set; }

        /// <summary>
        /// Gets or sets the number of files.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Number of files")]
        public long NumberOfFiles { get; set; }

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
