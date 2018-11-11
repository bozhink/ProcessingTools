// <copyright file="MediatypeCreateViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Files.Mediatypes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Mediatypes;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Mediatype create view model.
    /// </summary>
    public class MediatypeCreateViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypeCreateViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public MediatypeCreateViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Create Mediatype")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfFileExtensionName)]
        [Display(Name = "Extension")]
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the MIME type.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfMimeTypeName)]
        [Display(Name = "MIME type")]
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the MIME subtype.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfMimeSubtypeName)]
        [Display(Name = "MIME subtype")]
        public string MimeSubtype { get; set; }

        /// <summary>
        /// Gets or sets the description of the mediatype.
        /// </summary>
        [MaxLength(ValidationConstants.MaximalLengthOfFileExtensionDescription)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the list of known MIME types.
        /// </summary>
        public IEnumerable<string> MimeTypes { get; set; }

        /// <summary>
        /// Gets or sets the list of known MIME subtypes.
        /// </summary>
        public IEnumerable<string> MimeSubtypes { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
