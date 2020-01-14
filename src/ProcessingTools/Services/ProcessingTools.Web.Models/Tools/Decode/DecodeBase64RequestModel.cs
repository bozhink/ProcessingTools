// <copyright file="DecodeBase64RequestModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Decode
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Decode Base64 request model.
    /// </summary>
    public class DecodeBase64RequestModel : IContent
    {
        /// <summary>
        /// Gets or sets the text content to be encoded.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }
    }
}
