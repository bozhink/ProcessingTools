// <copyright file="ReferenceParseStyleIndexViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.References
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Reference parse style index view model.
    /// </summary>
    public class ReferenceParseStyleIndexViewModel
    {
        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the style.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the style.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the XPath for selection of the XML objects which represent the reference.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Reference XPath")]
        public string ReferenceXPath { get; set; }

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
