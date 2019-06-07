﻿// <copyright file="HashContentRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Hashes
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Hash content request model.
    /// </summary>
    public class HashContentRequestModel : IContent
    {
        /// <summary>
        /// Gets or sets the text content to be encoded.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }
    }
}
