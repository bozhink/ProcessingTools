// <copyright file="FileDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Files
{
    using System;

    /// <summary>
    /// File delete request model.
    /// </summary>
    public class FileDeleteRequestModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <inheritdoc/>
        public Uri ReturnUrl { get; set; }
    }
}
