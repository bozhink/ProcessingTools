namespace ProcessingTools.Services.Data.Services.Bio.Biorepositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Services.Bio.Biorepositories;
    using ProcessingTools.Contracts.Services.Models.Bio.Biorepositories;
    using ProcessingTools.Data.Models.Mongo.Bio.Biorepositories;
    using ProcessingTools.Data.Mongo.Bio.Biorepositories;

    public class BiorepositoriesPersonalCollectionsDataService : IBiorepositoriesPersonalCollectionsDataService
    {
        private readonly IBiorepositoriesRepository<CollectionPer> repository;

        public BiorepositoriesPersonalCollectionsDataService(IBiorepositoriesRepository<CollectionPer> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
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

            return Task.Run(() => this.GetData(skip, take));
        }

        private ICollection[] GetData(int skip, int take)
        {
            return this.repository.Query
                .Where(c => c.CollectionCode.Length > 1 && c.CollectionName.Length > 1)
                .OrderBy(i => i.Id)
                .Skip(skip)
                .Take(take)
                .Select(c => new ProcessingTools.Services.Models.Data.Bio.Biorepositories.Collection
                {
                    Code = c.CollectionCode,
                    Name = c.CollectionName,
                    Url = c.Url,
                })
                .ToArray<ICollection>();
        }
    }
}
