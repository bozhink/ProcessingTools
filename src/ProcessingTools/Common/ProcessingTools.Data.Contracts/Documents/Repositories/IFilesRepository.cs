namespace ProcessingTools.Contracts.Data.Documents.Repositories
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Documents.Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IFilesRepository : IRepository
    {
        Task<object> Add(IFileEntity entity);

        Task<IFileEntity> Get(object id);

        Task<object> Remove(object id);

        Task<object> Update(IFileEntity entity);
    }
}
