namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IRepositoryDataService<TDbModel, TServiceModel>
    {
        Task<int> Add(IGenericRepository<TDbModel> repository, params TServiceModel[] models);

        Task<IQueryable<TServiceModel>> All(IGenericRepository<TDbModel> repository);

        Task<int> Delete(IGenericRepository<TDbModel> repository, params TServiceModel[] models);

        Task<int> Delete(IGenericRepository<TDbModel> repository, params object[] ids);

        Task<IQueryable<TServiceModel>> Get(IGenericRepository<TDbModel> repository, params object[] ids);

        Task<IQueryable<TServiceModel>> Query(IGenericRepository<TDbModel> repository, int skip, int take);

        Task<int> Update(IGenericRepository<TDbModel> repository, params TServiceModel[] models);
    }
}
