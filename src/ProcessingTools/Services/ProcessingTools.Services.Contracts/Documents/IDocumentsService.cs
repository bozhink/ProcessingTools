// <copyright file="IDocumentsService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;

    /// <summary>
    /// Documents service.
    /// </summary>
    public interface IDocumentsService
    {
        /// <summary>
        /// Uploads new document from file..
        /// </summary>
        /// <param name="model">Document file model.</param>
        /// <param name="stream">Stream of the document's content.</param>
        /// <param name="articleId">Object ID of the article.</param>
        /// <returns>Resultant object.</returns>
        Task<object> UploadAsync(IDocumentFileModel model, Stream stream, string articleId);
    }
}
