namespace ProcessingTools.Bio.Biorepositories.Services.Data.Factories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Models.Contracts;

    public abstract class BiorepositoriesDataServiceFactory<TEntity, TModel> : IBiorepositoriesDataService<TModel>
        where TEntity : class, IStringIdentifiable
        where TModel : class
    {
        private readonly IBiorepositoriesRepositoryProvider<TEntity> repositoryProvider;

        protected BiorepositoriesDataServiceFactory(IBiorepositoriesRepositoryProvider<TEntity> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        protected abstract Expression<Func<TEntity, bool>> Filter { get; }

        protected abstract Expression<Func<TEntity, TModel>> Project { get; }

        public Task<TModel[]> GetAsync(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (take < 1 || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            return Task.Run(() =>
            {
                var repository = this.repositoryProvider.Create();

                var data = repository.Query
                    .Where(this.Filter)
                    .OrderBy(i => i.Id)
                    .Skip(skip)
                    .Take(take)
                    .Select(this.Project)
                    .ToArray();

                repository.TryDispose();

                return data;
            });
        }
    }
}
