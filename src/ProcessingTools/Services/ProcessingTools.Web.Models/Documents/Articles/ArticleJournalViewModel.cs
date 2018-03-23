// <copyright file="ArticleJournalViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Articles
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Article journal view model.
    /// </summary>
    public class ArticleJournalViewModel
    {
        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Journal ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the journal's abbreviated name.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Journal abbreviated name")]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the journal's name.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Journal name")]
        public string Name { get; set; }
    }
}
