namespace ProcessingTools.Bio.Biorepositories.Services.Data.Factories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Extensions.Linq;

    public abstract class BiorepositoriesDataServiceFactory<TDbModel, TServiceModel> : IBiorepositoriesDataService<TServiceModel>
        where TDbModel : class, IStringIdentifiable
        where TServiceModel : class
    {
        private readonly IBiorepositoriesRepositoryProvider<TDbModel> repositoryProvider;

        public BiorepositoriesDataServiceFactory(IBiorepositoriesRepositoryProvider<TDbModel> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        protected abstract Expression<Func<TDbModel, bool>> Filter { get; }

        protected abstract Expression<Func<TDbModel, TServiceModel>> Project { get; }

        public async Task<IQueryable<TServiceModel>> Get(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            var repository = this.repositoryProvider.Create();

            var result = await repository.Query
                .Where(this.Filter)
                .OrderBy(i => i.Id)
                .Skip(skip)
                .Take(take)
                .Select(this.Project)
                .ToListAsync();

            repository.TryDispose();

            return result.AsQueryable();
        }
    }
}
