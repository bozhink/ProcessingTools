// <copyright file="ReferenceParseStyleDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.References
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Reference parse style delete request model.
    /// </summary>
    public class ReferenceParseStyleDeleteRequestModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        /// <inheritdoc/>
        public Uri ReturnUrl { get; set; }
    }
}
