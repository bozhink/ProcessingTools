// <copyright file="EncodeBase64ViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Encode
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Encode Base64 view model.
    /// </summary>
    public class EncodeBase64ViewModel : IContent, IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncodeBase64ViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public EncodeBase64ViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Encode To Base64 String")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the text content to be encoded.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the Base64 encoded resultant string.
        /// </summary>
        public string Base64EncodedString { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
