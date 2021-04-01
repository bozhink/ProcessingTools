// <copyright file="FilesIndexViewModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Files
{
    using System;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Files index view model.
    /// </summary>
    public class FilesIndexViewModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <summary>
        /// Gets or sets the User Context.
        /// </summary>
        public UserContext UserContext { get; set; }

        /// <inheritdoc/>
        public Uri ReturnUrl { get; set; }
    }
}
