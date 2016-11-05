namespace ProcessingTools.Documents.Data.Common.Contracts.Repositories
{
    using System.Threading.Tasks;
    using Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IFilesRepository : ISavabaleRepository
    {
        Task<object> Add(IFileEntity entity);

        Task<IFileEntity> Get(object id);

        Task<object> Remove(object id);

        Task<object> Update(IFileEntity entity);
    }
}
