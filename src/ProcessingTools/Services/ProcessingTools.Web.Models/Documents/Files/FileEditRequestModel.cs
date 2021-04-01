// <copyright file="FileEditRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Files
{
    using System;

    /// <summary>
    /// File edit request model.
    /// </summary>
    public class FileEditRequestModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <inheritdoc/>
        public Uri ReturnUrl { get; set; }
    }
}
