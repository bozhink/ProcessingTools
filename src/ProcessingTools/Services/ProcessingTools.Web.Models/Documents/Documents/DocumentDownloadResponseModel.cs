// <copyright file="DocumentDownloadResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Documents.Documents;

namespace ProcessingTools.Web.Models.Documents.Documents
{
    using System.IO;

    /// <summary>
    /// Document download response model.
    /// </summary>
    public class DocumentDownloadResponseModel : IDocumentFileStreamModel
    {
        /// <summary>
        /// Gets or sets the content type of the file.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the content length of the file.
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the content stream.
        /// </summary>
        public Stream Stream { get; set; }
    }
}
