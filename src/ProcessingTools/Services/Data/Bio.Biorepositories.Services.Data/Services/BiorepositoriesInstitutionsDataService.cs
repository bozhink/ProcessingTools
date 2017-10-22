namespace ProcessingTools.Bio.Biorepositories.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Exceptions;

    public class BiorepositoriesInstitutionsDataService : IBiorepositoriesInstitutionsDataService
    {
        private readonly IBiorepositoriesRepositoryProvider<Biorepositories.Data.Mongo.Models.Institution> repositoryProvider;

        public BiorepositoriesInstitutionsDataService(IBiorepositoriesRepositoryProvider<Biorepositories.Data.Mongo.Models.Institution> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public Task<Models.Institution[]> GetAsync(int skip, int take)
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
                    .Where(i => i.InstitutionCode.Length > 1 && i.InstitutionName.Length > 1)
                    .OrderBy(i => i.Id)
                    .Skip(skip)
                    .Take(take)
                    .Select(i => new Models.Institution
                    {
                        Code = i.InstitutionCode,
                        Name = i.InstitutionName,
                        Url = i.Url
                    })
                    .ToArray();

                repository.TryDispose();

                return data;
            });
        }
    }
}
