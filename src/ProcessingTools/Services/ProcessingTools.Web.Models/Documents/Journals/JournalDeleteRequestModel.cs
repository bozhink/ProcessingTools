// <copyright file="JournalDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Journals
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Journal delete request model.
    /// </summary>
    public class JournalDeleteRequestModel : ProcessingTools.Contracts.Models.IWebModel
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
