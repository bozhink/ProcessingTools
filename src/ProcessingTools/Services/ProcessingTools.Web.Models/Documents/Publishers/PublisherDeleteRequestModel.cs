// <copyright file="PublisherDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Publishers
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Publisher delete request model.
    /// </summary>
    public class PublisherDeleteRequestModel : ProcessingTools.Models.Contracts.IWebModel
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
