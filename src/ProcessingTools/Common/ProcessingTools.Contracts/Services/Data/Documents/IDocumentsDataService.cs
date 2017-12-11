// <copyright file="IDocumentsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Documents
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Models.Services.Data.Documents;

    /// <summary>
    /// Documents data service.
    /// </summary>
    public interface IDocumentsDataService
    {
        Task<IDocument[]> AllAsync(object userId, object articleId, int pageNumber, int itemsPerPage);

        Task<long> CountAsync(object userId, object articleId);

        Task<object> CreateAsync(object userId, object articleId, IDocument document, Stream inputStream);

        Task<object> DeleteAsync(object userId, object articleId, object documentId);

        Task<object> DeleteAllAsync(object userId, object articleId);

        Task<IDocument> GetAsync(object userId, object articleId, object documentId);

        Task<XmlReader> GetReaderAsync(object userId, object articleId, object documentId);

        Task<Stream> GetStreamAsync(object userId, object articleId, object documentId);

        Task<object> UpdateMetaAsync(object userId, object articleId, IDocument document);

        Task<object> UpdateContentAsync(object userId, object articleId, IDocument document, string content);
    }
}
