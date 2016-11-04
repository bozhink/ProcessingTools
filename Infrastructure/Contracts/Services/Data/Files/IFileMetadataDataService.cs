namespace ProcessingTools.Contracts.Services.Data.Files
{
    using System.Threading.Tasks;
    using Models.Files;

    public interface IFileMetadataDataService
    {
        IFileMetadata Get(object id);

        Task<object> Update(object id, IFileMetadata data);
    }
}
