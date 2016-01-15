namespace ProcessingTools.Bio.Biorepositories.Services.Data
{
    using System.Linq;

    using Contracts;
    using Models;
    using Models.Contracts;
    using ProcessingTools.Bio.Biorepositories.Data;
    using ProcessingTools.Bio.Biorepositories.Data.Repositories;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;

    public class BiorepositoriesDataService : IBiorepositoriesDataService
    {
        private IBiorepositoriesDbFirstGenericRepository<Biorepository> repository;

        public BiorepositoriesDataService(IBiorepositoriesDbFirstGenericRepository<Biorepository> repository)
        {
            this.repository = repository;
        }

        public IQueryable<IBiorepositoryInstitutionalCode> GetBiorepositoryInstitutionalCodes(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            return this.repository.All()
                .Where(i => i.InstitutionalCode.Length > 1 && i.NameOfInstitution.Length > 1)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(i => new BiorepositoryInstitutionalCodeDataServiceResponseModel
                {
                    InstitutionalCode = i.InstitutionalCode,
                    NameOfInstitution = i.NameOfInstitution,
                    Url = i.Url
                })
                .AsQueryable();
        }

        public IQueryable<IBiorepositoryInstitution> GetBiorepositoryInstitutions(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            return this.repository.All()
                .Where(i => i.InstitutionalCode.Length > 1 && i.NameOfInstitution.Length > 1)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(i => new BiorepositoryInstitutionDataServiceResponseModel
                {
                    Name = i.NameOfInstitution,
                    Url = i.Url
                })
                .AsQueryable();
        }
    }
}
