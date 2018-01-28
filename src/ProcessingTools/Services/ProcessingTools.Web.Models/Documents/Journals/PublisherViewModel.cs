// <copyright file="PublisherViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Journals
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Publisher view model.
    /// </summary>
    public class PublisherViewModel
    {
        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Publisher ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the publisher's abbreviated name.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Publisher abbreviated name")]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the publisher's name.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Publisher name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether publisher is selected.
        /// </summary>
        public bool Selected { get; set; }
    }
}
