namespace ProcessingTools.Contracts.Services.Data.Files
{
    using System.IO;
    using System.Threading.Tasks;
    using Models.Files;

    public interface IStreamingFilesDataService
    {
        Task<IFileMetadata> Create(IFileMetadata metadata, Stream stream);

        Task<bool> Delete(object id);

        Task<IFileMetadata> GetMetadata(object id);

        StreamReader GetReader(object id);

        Stream ReadToStream(object id);

        Task<IFileMetadata> Update(object id, Stream stream);

        Task<IFileMetadata> Update(IFileMetadata metadata, Stream stream);
    }
}
