// <copyright file="DecodeBase64ViewModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Decode
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Decode Base64 view model.
    /// </summary>
    public class DecodeBase64ViewModel : IContent, IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecodeBase64ViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public DecodeBase64ViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Decode Base64 String")]
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
        /// Gets or sets the Base64 decoded resultant string.
        /// </summary>
        public string Base64DecodedString { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
