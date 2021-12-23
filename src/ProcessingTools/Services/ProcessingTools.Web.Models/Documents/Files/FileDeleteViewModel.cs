// <copyright file="FileDeleteViewModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Files
{
    using System;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// File delete view model.
    /// </summary>
    public class FileDeleteViewModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <summary>
        /// Gets or sets the User Context.
        /// </summary>
        public UserContext UserContext { get; set; }

        /// <inheritdoc/>
        public Uri ReturnUrl { get; set; }
    }
}
