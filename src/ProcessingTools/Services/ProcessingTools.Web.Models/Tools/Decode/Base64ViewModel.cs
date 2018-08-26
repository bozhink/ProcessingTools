// <copyright file="Base64ViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Decode
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Decode Base64 view model.
    /// </summary>
    public class Base64ViewModel : IContent
    {
        /// <summary>
        /// Gets or sets the text content to be encoded.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the Base64 decoded resultant string.
        /// </summary>
        public string Base64DecodedString { get; set; }
    }
}
