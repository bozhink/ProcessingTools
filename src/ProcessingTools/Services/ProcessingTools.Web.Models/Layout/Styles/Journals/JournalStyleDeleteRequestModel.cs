// <copyright file="JournalStyleDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Journals
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Journal style delete request model.
    /// </summary>
    public class JournalStyleDeleteRequestModel : ProcessingTools.Contracts.Models.IWebModel
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
