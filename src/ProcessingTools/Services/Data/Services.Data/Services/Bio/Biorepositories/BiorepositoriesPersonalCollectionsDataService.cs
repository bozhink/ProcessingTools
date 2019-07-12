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

    /// <summary>
    /// Biorepositories personal collections data service.
    /// </summary>
    public class BiorepositoriesPersonalCollectionsDataService : IBiorepositoriesPersonalCollectionsDataService
    {
        private readonly IBiorepositoriesRepository<CollectionPer> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BiorepositoriesPersonalCollectionsDataService"/> class.
        /// </summary>
        /// <param name="repository">Repository.</param>
        public BiorepositoriesPersonalCollectionsDataService(IBiorepositoriesRepository<CollectionPer> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public Task<ICollection[]> GetAsync(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException($"Invalid skip value = {skip}");
            }

            if (take < 1 || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException($"Invalid take value = {take}");
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
