namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using Models;

    public interface IDocumentsDataService
    {
        Task<IEnumerable<DocumentServiceModel>> All(object userId, object articleId, int pageNumber, int itemsPerPage);

        Task<long> Count(object userId, object articleId);

        Task<object> Create(object userId, object articleId, DocumentServiceModel document, Stream inputStream);

        Task<object> Delete(object userId, object articleId, object documentId);

        Task<object> DeleteAll(object userId, object articleId);

        Task<DocumentServiceModel> Get(object userId, object articleId, object documentId);

        Task<XmlReader> GetReader(object userId, object articleId, object documentId);

        Task<Stream> GetStream(object userId, object articleId, object documentId);

        Task<object> UpdateMeta(object userId, object articleId, DocumentServiceModel document);

        Task<object> UpdateContent(object userId, object articleId, DocumentServiceModel document, string content);
    }
}
