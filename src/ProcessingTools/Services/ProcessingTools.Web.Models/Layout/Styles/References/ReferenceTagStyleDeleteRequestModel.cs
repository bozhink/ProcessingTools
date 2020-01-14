// <copyright file="ReferenceTagStyleDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.References
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Reference tag style delete request model.
    /// </summary>
    public class ReferenceTagStyleDeleteRequestModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
