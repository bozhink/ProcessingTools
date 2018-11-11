namespace ProcessingTools.Bio.Biorepositories.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Models;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Contracts.Bio.Biorepositories;
    using ProcessingTools.Services.Models.Contracts.Bio.Biorepositories;

    public class BiorepositoriesPersonalCollectionsDataService : IBiorepositoriesPersonalCollectionsDataService
    {
        private readonly IBiorepositoriesRepositoryProvider<CollectionPer> repositoryProvider;

        public BiorepositoriesPersonalCollectionsDataService(IBiorepositoriesRepositoryProvider<CollectionPer> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public Task<ICollection[]> GetAsync(int skip, int take)
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
                    .Where(c => c.CollectionCode.Length > 1 && c.CollectionName.Length > 1)
                    .OrderBy(i => i.Id)
                    .Skip(skip)
                    .Take(take)
                    .Select(c => new ProcessingTools.Services.Models.Data.Bio.Biorepositories.Collection
                    {
                        Code = c.CollectionCode,
                        Name = c.CollectionName,
                        Url = c.Url
                    })
                    .ToArray<ICollection>();

                repository.TryDispose();

                return data;
            });
        }
    }
}
