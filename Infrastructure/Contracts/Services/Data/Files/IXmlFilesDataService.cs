namespace ProcessingTools.Contracts.Services.Data.Files
{
    using System.Threading.Tasks;
    using System.Xml;
    using IO;
    using Models.Files;

    public interface IXmlFilesDataService : IXmlFileContentReader
    {
        XmlWriterSettings WriterSettings { get; set; }

        Task<IFileMetadata> Create(IFileMetadata metadata, XmlReader reader);

        Task<IFileMetadata> Update(object id, XmlReader reader);

        Task<IFileMetadata> Update(IFileMetadata metadata, XmlReader reader);

        Task<bool> Delete(object id);
    }
}
