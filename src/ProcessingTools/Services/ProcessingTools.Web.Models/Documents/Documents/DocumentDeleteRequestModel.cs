// <copyright file="DocumentDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Documents
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Document delete request model.
    /// </summary>
    public class DocumentDeleteRequestModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the object ID of the article.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string ArticleId { get; set; }

        /// <inheritdoc/>
        public Uri ReturnUrl { get; set; }
    }
}
