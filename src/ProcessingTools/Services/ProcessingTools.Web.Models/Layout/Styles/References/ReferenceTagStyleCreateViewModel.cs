// <copyright file="ReferenceTagStyleCreateViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.References
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Reference tag style create view model.
    /// </summary>
    public class ReferenceTagStyleCreateViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTagStyleCreateViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public ReferenceTagStyleCreateViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Create Reference Tag Style")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the name of the style.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the style.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the script content.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
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
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Target XPath")]
        public string TargetXPath { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
