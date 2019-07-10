using System;
using System.Linq;
using System.Threading.Tasks;
using ProcessingTools.Common.Constants;
using ProcessingTools.Common.Exceptions;
using ProcessingTools.Data.Models.Mongo.Bio.Biorepositories;
using ProcessingTools.Data.Mongo.Bio.Biorepositories;
using ProcessingTools.Services.Contracts.Bio.Biorepositories;
using ProcessingTools.Services.Models.Contracts.Bio.Biorepositories;

namespace ProcessingTools.Services.Data.Services.Bio.Biorepositories
{
    public class BiorepositoriesInstitutionsDataService : IBiorepositoriesInstitutionsDataService
    {
        private readonly IBiorepositoriesRepository<Institution> repository;

        public BiorepositoriesInstitutionsDataService(IBiorepositoriesRepository<Institution> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<IInstitution[]> GetAsync(int skip, int take)
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

        private IInstitution[] GetData(int skip, int take)
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
                .ToArray<IInstitution>();
        }
    }
}
