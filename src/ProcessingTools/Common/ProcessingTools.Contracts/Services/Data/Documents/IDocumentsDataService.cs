// <copyright file="IDocumentsDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Documents
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Models.Services.Data.Documents;

    public interface IDocumentsDataService
    {
        Task<IEnumerable<IDocument>> All(object userId, object articleId, int pageNumber, int itemsPerPage);

        Task<long> Count(object userId, object articleId);

        Task<object> Create(object userId, object articleId, IDocument document, Stream inputStream);

        Task<object> Delete(object userId, object articleId, object documentId);

        Task<object> DeleteAll(object userId, object articleId);

        Task<IDocument> Get(object userId, object articleId, object documentId);

        Task<XmlReader> GetReader(object userId, object articleId, object documentId);

        Task<Stream> GetStream(object userId, object articleId, object documentId);

        Task<object> UpdateMeta(object userId, object articleId, IDocument document);

        Task<object> UpdateContent(object userId, object articleId, IDocument document, string content);
    }
}
