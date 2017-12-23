namespace ProcessingTools.Bio.Biorepositories.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Models.Services.Data.Bio.Biorepositories;
    using ProcessingTools.Contracts.Services.Data.Bio.Biorepositories;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Extensions;

    public class BiorepositoriesInstitutionalCollectionsDataService : IBiorepositoriesInstitutionalCollectionsDataService
    {
        private readonly IBiorepositoriesRepositoryProvider<Biorepositories.Data.Mongo.Models.Collection> repositoryProvider;

        public BiorepositoriesInstitutionalCollectionsDataService(IBiorepositoriesRepositoryProvider<Biorepositories.Data.Mongo.Models.Collection> repositoryProvider)
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
