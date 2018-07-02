// <copyright file="DocumentArticleViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Documents
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Document article view model.
    /// </summary>
    public class DocumentArticleViewModel
    {
        /// <summary>
        /// Gets or sets the object ID of the article.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Article")]
        public string ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the title of the article.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Article title")]
        public string ArticleTitle { get; set; }

        /// <summary>
        /// Gets or sets the object ID of the journal.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Journal")]
        public string JournalId { get; set; }

        /// <summary>
        /// Gets or sets the name of the journal.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Journal name")]
        public string JournalName { get; set; }
    }
}
