// <copyright file="ReferenceParseStyleCreateViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.References
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Reference parse style create view model.
    /// </summary>
    public class ReferenceParseStyleCreateViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceParseStyleCreateViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public ReferenceParseStyleCreateViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Create Reference Parse Style")]
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
        public string Script { get; set; } = @"# Enter rule sets
- xPath: /*
  rules:
    - pattern: ^1
      replacement: 1
";

        /// <summary>
        /// Gets or sets the XPath for selection of the XML objects which represent the reference.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Reference XPath")]
        public string ReferenceXPath { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
