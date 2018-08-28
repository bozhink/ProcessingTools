﻿// <copyright file="EncodeBase64UrlViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Encode
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Encode Base64 URL view model.
    /// </summary>
    public class EncodeBase64UrlViewModel : IContent
    {
        /// <summary>
        /// Gets or sets the text content to be encoded.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the Base64 encoded resultant string.
        /// </summary>
        public string Base64EncodedString { get; set; }
    }
}
