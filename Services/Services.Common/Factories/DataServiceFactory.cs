namespace ProcessingTools.Services.Common.Factories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Common.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;
    using ProcessingTools.Extensions;

    public abstract class DataServiceFactory<TDbModel, TServiceModel> : DataServiceBaseFactory<TDbModel, TServiceModel>, IDataService<TServiceModel>
        where TDbModel : class
        where TServiceModel : class
    {
        public DataServiceFactory(IGenericRepositoryProvider<IGenericRepository<TDbModel>, TDbModel> repositoryProvider)
            : base(repositoryProvider)
        {
        }

        public virtual async Task<IQueryable<TServiceModel>> All()
        {
            var repository = this.repositoryProvider.Create();

            var result = (await repository.All())
                .Select(this.MapDataToServiceModel)
                .ToList();

            repository.TryDispose();

            return result.AsQueryable();
        }
    }
}
