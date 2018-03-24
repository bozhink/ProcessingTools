// <copyright file="FileDetailsViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Files
{
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// File Details View Model
    /// </summary>
    public class FileDetailsViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Gets or sets the User Context.
        /// </summary>
        public UserContext UserContext { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
