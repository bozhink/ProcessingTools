// <copyright file="FloatObjectTagStyleDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Floats
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Float object tag style delete request model.
    /// </summary>
    public class FloatObjectTagStyleDeleteRequestModel : ProcessingTools.Models.Contracts.IWebModel
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
