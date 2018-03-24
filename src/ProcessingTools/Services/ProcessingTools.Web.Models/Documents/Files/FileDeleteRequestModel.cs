// <copyright file="FileDeleteRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Files
{
    /// <summary>
    /// File Delete Request Model
    /// </summary>
    public class FileDeleteRequestModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
