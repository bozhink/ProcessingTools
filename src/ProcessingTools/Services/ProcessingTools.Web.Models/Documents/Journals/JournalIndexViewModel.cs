// <copyright file="JournalIndexViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Journals
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Journal index view model.
    /// </summary>
    public class JournalIndexViewModel
    {
        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the journal's abbreviated name.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Abbreviated name")]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the journal's name.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the journal's ID.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Journal ID")]
        public string JournalId { get; set; }

        /// <summary>
        /// Gets or sets the journal's print ISSN.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Print ISSN")]
        public string PrintIssn { get; set; }

        /// <summary>
        /// Gets or sets the journal's electronic ISSN.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Electronic ISSN")]
        public string ElectronicIssn { get; set; }

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

        /// <summary>
        /// Gets or sets the selected publisher.
        /// </summary>
        [ReadOnly(true)]
        public JournalPublisherViewModel Publisher { get; set; } = new JournalPublisherViewModel();
    }
}
