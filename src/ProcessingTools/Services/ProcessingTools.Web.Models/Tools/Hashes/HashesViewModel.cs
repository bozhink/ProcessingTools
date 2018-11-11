// <copyright file="HashesViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Hashes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Hashes view model.
    /// </summary>
    public class HashesViewModel : IContent, IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HashesViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public HashesViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Evaluate Hashes")]
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
        /// Gets or sets the MD5 hash of the content as binary string.
        /// </summary>
        public string MD5String { get; set; }

        /// <summary>
        /// Gets or sets the MD5 hash of the content as Base64 string.
        /// </summary>
        public string MD5Base64String { get; set; }

        /// <summary>
        /// Gets or sets the SHA1 hash of the content as binary string.
        /// </summary>
        public string SHA1String { get; set; }

        /// <summary>
        /// Gets or sets the SHA1 hash of the content as Base64 string.
        /// </summary>
        public string SHA1Base64String { get; set; }

        /// <summary>
        /// Gets or sets the SHA256 hash of the content as binary string.
        /// </summary>
        public string SHA256String { get; set; }

        /// <summary>
        /// Gets or sets the SHA256 hash of the content as Base64 string.
        /// </summary>
        public string SHA256Base64String { get; set; }

        /// <summary>
        /// Gets or sets the SHA384 hash of the content as binary string.
        /// </summary>
        public string SHA384String { get; set; }

        /// <summary>
        /// Gets or sets the SHA384 hash of the content as Base54 string.
        /// </summary>
        public string SHA384Base64String { get; set; }

        /// <summary>
        /// Gets or sets the SHA512 hash of the content as binary string.
        /// </summary>
        public string SHA512String { get; set; }

        /// <summary>
        /// Gets or sets the SHA512 hash of the content as Base64 string.
        /// </summary>
        public string SHA512Base64String { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
