namespace ProcessingTools.Services.Common.Factories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;

    public abstract class DataServiceFactory<TDbModel, TServiceModel> : DataServiceBaseFactory<TDbModel, TServiceModel>, IDataService<TServiceModel>
        where TDbModel : class
        where TServiceModel : class
    {
        public DataServiceFactory(ISearchableCountableCrudRepositoryProvider<TDbModel> repositoryProvider)
            : base(repositoryProvider)
        {
        }

        public virtual async Task<IQueryable<TServiceModel>> All()
        {
            var repository = this.RepositoryProvider.Create();

            var result = await repository.Query
                .Select(this.MapDataToServiceModel)
                .ToListAsync();

            repository.TryDispose();

            return result.AsQueryable();
        }
    }
}
