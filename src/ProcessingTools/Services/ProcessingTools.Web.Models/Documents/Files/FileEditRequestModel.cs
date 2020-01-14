// <copyright file="FileEditRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Files
{
    /// <summary>
    /// File edit request model.
    /// </summary>
    public class FileEditRequestModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
