// <copyright file="MediatypeDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Files.Mediatypes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Mediatype delete request model.
    /// </summary>
    public class MediatypeDeleteRequestModel : ProcessingTools.Contracts.Models.IWebModel
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
