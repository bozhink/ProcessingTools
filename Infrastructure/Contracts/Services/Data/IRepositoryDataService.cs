namespace ProcessingTools.Contracts.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IRepositoryDataService<TDbModel, TServiceModel>
    {
        Task<object> Add(ISearchableCountableCrudRepository<TDbModel> repository, params TServiceModel[] models);

        Task<IQueryable<TServiceModel>> All(ISearchableCountableCrudRepository<TDbModel> repository);

        Task<object> Delete(ISearchableCountableCrudRepository<TDbModel> repository, params TServiceModel[] models);

        Task<object> Delete(ISearchableCountableCrudRepository<TDbModel> repository, params object[] ids);

        Task<IQueryable<TServiceModel>> Get(ISearchableCountableCrudRepository<TDbModel> repository, params object[] ids);

        Task<IQueryable<TServiceModel>> Query(ISearchableCountableCrudRepository<TDbModel> repository, int skip, int take);

        Task<object> Update(ISearchableCountableCrudRepository<TDbModel> repository, params TServiceModel[] models);
    }
}
