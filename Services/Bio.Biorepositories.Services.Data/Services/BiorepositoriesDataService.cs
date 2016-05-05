namespace ProcessingTools.Bio.Biorepositories.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Models;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Extensions;

    public class BiorepositoriesDataService : IBiorepositoriesDataService
    {
        private IBiorepositoriesRepositoryProvider<Institution> repositoryProvider;

        public BiorepositoriesDataService(IBiorepositoriesRepositoryProvider<Institution> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        public async Task<IQueryable<BiorepositoryInstitutionServiceModel>> GetInstitutions(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            var repository = this.repositoryProvider.Create();

            var result = (await repository.All())
                .Where(i => i.InstitutionCode.Length > 1 && i.InstitutionName.Length > 1)
                .OrderBy(i => i.Id)
                .Skip(skip)
                .Take(take)
                .Select(i => new BiorepositoryInstitutionServiceModel
                {
                    InstitutionalCode = i.InstitutionCode,
                    NameOfInstitution = i.InstitutionName,
                    Url = i.Url
                })
                .ToList();

            repository.TryDispose();

            return result.AsQueryable();
        }
    }
}
