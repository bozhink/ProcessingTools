namespace ProcessingTools.Services.Data.Services.Bio.Biorepositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;
    using ProcessingTools.Contracts.Services.Bio.Biorepositories;
    using ProcessingTools.Data.Models.Mongo.Bio.Biorepositories;
    using ProcessingTools.Data.Mongo.Bio.Biorepositories;

    /// <summary>
    /// Biorepositories institutions data service.
    /// </summary>
    public class BiorepositoriesInstitutionsDataService : IBiorepositoriesInstitutionsDataService
    {
        private readonly IBiorepositoriesRepository<Institution> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BiorepositoriesInstitutionsDataService"/> class.
        /// </summary>
        /// <param name="repository">Repository.</param>
        public BiorepositoriesInstitutionsDataService(IBiorepositoriesRepository<Institution> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public Task<IInstitutionMetaModel[]> GetAsync(int skip, int take)
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

        private IInstitutionMetaModel[] GetData(int skip, int take)
        {
            return this.repository.Query
                .Where(i => i.InstitutionCode.Length > 1 && i.InstitutionName.Length > 1)
                .OrderBy(i => i.Id)
                .Skip(skip)
                .Take(take)
                .Select(i => new ProcessingTools.Services.Models.Data.Bio.Biorepositories.Institution
                {
                    Code = i.InstitutionCode,
                    Name = i.InstitutionName,
                    Url = i.Url,
                })
                .ToArray<IInstitutionMetaModel>();
        }
    }
}
