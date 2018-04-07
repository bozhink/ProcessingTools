// <copyright file="ArticleDocumentViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Articles
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Article document view model.
    /// </summary>
    public class ArticleDocumentViewModel
    {
        /// <summary>
        /// Gets or sets the document ID.
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// Gets or sets the article ID.
        /// </summary>
        public string ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the file ID.
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// Gets or sets the description of the document.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether document is final.
        /// </summary>
        public bool IsFinal { get; set; }

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
    }
}
