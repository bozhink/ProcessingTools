// <copyright file="HashesViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Hashes
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Hashes view model.
    /// </summary>
    public class HashesViewModel : IContent
    {
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
    }
}
