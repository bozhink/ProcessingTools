namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Models;

    public interface IDocumentsDataService
    {
        Task<IQueryable<DocumentServiceModel>> All(object userId, object articleId, int pageNumber, int itemsPerPage);

        Task<long> Count(object userId, object articleId);

        Task<object> Create(object userId, object articleId, DocumentServiceModel fileMetadata, Stream inputStream);

        Task<object> Delete(object userId, object articleId, object fileId);

        Task<DocumentServiceModel> Get(object userId, object articleId, object fileId);

        Task<XmlReader> GetReader(object userId, object articleId, object fileId);

        Task<Stream> GetStream(object userId, object articleId, object fileId);

        Task<object> Update(object userId, object articleId, DocumentServiceModel file, string content);
    }
}
