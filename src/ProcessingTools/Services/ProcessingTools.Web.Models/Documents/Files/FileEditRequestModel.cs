// <copyright file="FileEditRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Files
{
    /// <summary>
    /// File Edit Request Model
    /// </summary>
    public class FileEditRequestModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
