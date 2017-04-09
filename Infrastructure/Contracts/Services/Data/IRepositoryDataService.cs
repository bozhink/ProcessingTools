namespace ProcessingTools.Contracts.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IRepositoryDataService<TDbModel, TServiceModel>
    {
        Task<object> Add(ICrudRepository<TDbModel> repository, params TServiceModel[] models);

        Task<IQueryable<TServiceModel>> All(ICrudRepository<TDbModel> repository);

        Task<object> Delete(ICrudRepository<TDbModel> repository, params TServiceModel[] models);

        Task<object> Delete(ICrudRepository<TDbModel> repository, params object[] ids);

        Task<IQueryable<TServiceModel>> Get(ICrudRepository<TDbModel> repository, params object[] ids);

        Task<IQueryable<TServiceModel>> Query(ICrudRepository<TDbModel> repository, int skip, int take);

        Task<object> Update(ICrudRepository<TDbModel> repository, params TServiceModel[] models);
    }
}
