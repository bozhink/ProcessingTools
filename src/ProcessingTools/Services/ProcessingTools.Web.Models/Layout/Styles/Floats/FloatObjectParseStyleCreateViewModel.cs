// <copyright file="FloatObjectParseStyleCreateViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Floats
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Float object parse style create view model.
    /// </summary>
    public class FloatObjectParseStyleCreateViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectParseStyleCreateViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public FloatObjectParseStyleCreateViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Create Float Object Parse Style")]
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

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
