﻿// <copyright file="FloatObjectParseStyleEditViewModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Floats
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Float object parse style edit view model.
    /// </summary>
    public class FloatObjectParseStyleEditViewModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectParseStyleEditViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public FloatObjectParseStyleEditViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Edit Float Object Parse Style")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "ID")]
        public string Id { get; set; }

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
        /// Gets or sets the reference type of the floating object according to NLM schema.
        /// </summary>
        [Required]
        [Display(Name = "Reference type")]
        public ReferenceType FloatReferenceType { get; set; }

        /// <summary>
        /// Gets or sets the script content.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Script")]
        public string Script { get; set; }

        /// <summary>
        /// Gets or sets the XPath for selection of the XML objects which provide information about the floating object.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Float object XPath")]
        public string FloatObjectXPath { get; set; }

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
