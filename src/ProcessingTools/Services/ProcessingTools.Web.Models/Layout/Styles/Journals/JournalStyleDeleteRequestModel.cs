// <copyright file="JournalStyleDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Journals
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Journal style delete request model.
    /// </summary>
    public class JournalStyleDeleteRequestModel : ProcessingTools.Models.Contracts.IWebModel
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
