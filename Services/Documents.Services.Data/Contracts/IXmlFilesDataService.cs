namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public interface IXmlFilesDataService
    {
        Task<IQueryable<XmlFileMetadataServiceModel>> All(object userId, object articleId, int pageNumber, int itemsPerPage);

        Task<object> Create(object userId, object articleId, XmlFileMetadataServiceModel fileMetadata, Stream inputStream);

        Task<object> Create(object userId, object articleId, XmlFileDetailsServiceModel file);

        Task<object> Delete(object userId, object articleId, object fileId);

        Task<XmlFileDetailsServiceModel> Get(object userId, object articleId, object fileId);

        Task<object> Update(object userId, object articleId, XmlFileDetailsServiceModel file);
    }
}
