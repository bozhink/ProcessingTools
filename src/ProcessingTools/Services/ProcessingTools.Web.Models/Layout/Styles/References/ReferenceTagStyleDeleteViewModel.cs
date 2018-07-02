// <copyright file="ReferenceTagStyleDeleteViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.References
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Reference tag style delete view model.
    /// </summary>
    public class ReferenceTagStyleDeleteViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTagStyleDeleteViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public ReferenceTagStyleDeleteViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Delete Reference Tag Style")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

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
        /// Gets or sets the script content.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Script")]
        public string Script { get; set; }

        /// <summary>
        /// Gets or sets the XPath for selection of the XML objects which represent the reference.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Reference XPath")]
        public string ReferenceXPath { get; set; }

        /// <summary>
        /// Gets or sets the target XPath.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Target XPath")]
        public string TargetXPath { get; set; }

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
        public string ReturnUrl { get; set; }
    }
}
