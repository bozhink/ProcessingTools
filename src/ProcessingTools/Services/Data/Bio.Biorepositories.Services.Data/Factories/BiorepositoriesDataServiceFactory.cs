namespace ProcessingTools.Bio.Biorepositories.Services.Data.Factories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Exceptions;

    public abstract class BiorepositoriesDataServiceFactory<TDbModel, TServiceModel> : IBiorepositoriesDataService<TServiceModel>
        where TDbModel : class, IStringIdentifiable
        where TServiceModel : class
    {
        private readonly IBiorepositoriesRepositoryProvider<TDbModel> repositoryProvider;

        protected BiorepositoriesDataServiceFactory(IBiorepositoriesRepositoryProvider<TDbModel> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        protected abstract Expression<Func<TDbModel, bool>> Filter { get; }

        protected abstract Expression<Func<TDbModel, TServiceModel>> Project { get; }

        public async Task<IQueryable<TServiceModel>> Get(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (take < 1 || take > PaginationConstants.MaximalItemsPerPageAllowed)
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
