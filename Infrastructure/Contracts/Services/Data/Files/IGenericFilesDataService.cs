namespace ProcessingTools.Contracts.Services.Data.Files
{
    using System.Threading.Tasks;
    using IO;
    using Models.Files;

    public interface IGenericFilesDataService<TContent> : IGenericFileContentReader<TContent>
    {
        Task<IFileMetadata> Create(IFileMetadata metadata, TContent content);

        Task<IFileMetadata> Update(object id, TContent content);

        Task<IFileMetadata> Update(IFileMetadata metadata, TContent content);

        Task<bool> Delete(object id);
    }
}
