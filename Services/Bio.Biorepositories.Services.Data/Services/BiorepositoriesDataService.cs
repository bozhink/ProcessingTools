namespace ProcessingTools.Bio.Biorepositories.Services.Data
{
    using System;
    using System.Linq;

    using Contracts;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Bio.Biorepositories.Data;
    using ProcessingTools.Bio.Biorepositories.Data.Repositories.Contracts;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;

    public class BiorepositoriesDataService : IBiorepositoriesDataService
    {
        private IBiorepositoriesDbFirstGenericRepository<Biorepository> repository;

        public BiorepositoriesDataService(IBiorepositoriesDbFirstGenericRepository<Biorepository> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public IQueryable<IBiorepositoryInstitutionalCodeServiceModel> GetBiorepositoryInstitutionalCodes(int skip, int take)
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
                .OrderBy(i => i.Id)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(i => new BiorepositoryInstitutionalCodeServiceModel
                {
                    InstitutionalCode = i.InstitutionalCode,
                    NameOfInstitution = i.NameOfInstitution,
                    Url = i.Url
                })
                .AsQueryable();
        }

        public IQueryable<IBiorepositoryInstitutionServiceModel> GetBiorepositoryInstitutions(int skip, int take)
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
                .OrderBy(i => i.Id)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(i => new BiorepositoryInstitutionServiceModel
                {
                    Name = i.NameOfInstitution,
                    Url = i.Url
                })
                .AsQueryable();
        }
    }
}
