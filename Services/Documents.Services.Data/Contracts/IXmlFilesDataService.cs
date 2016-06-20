namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public interface IXmlFilesDataService
    {
        Task<IQueryable<XmlFileMetadataServiceModel>> All();

        Task<string> GetXmlFileContent(object id);

        Task<object> Delete(object id);

        Task<object> Create(XmlFileMetadataServiceModel fileMetadata, Stream inputStream);
    }
}
