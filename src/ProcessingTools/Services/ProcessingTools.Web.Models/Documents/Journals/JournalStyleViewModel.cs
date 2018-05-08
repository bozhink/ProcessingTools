// <copyright file="JournalStyleViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Journals
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Journal style view model.
    /// </summary>
    public class JournalStyleViewModel
    {
        /// <summary>
        /// Gets or sets the object ID of the style.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the style.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Journal style name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the style.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Journal style description")]
        public string Description { get; set; }
    }
}
