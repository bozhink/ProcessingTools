namespace ProcessingTools.Bio.Environments.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Environments.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Bio.Environments.Data.Entity.Models;
    using ProcessingTools.Bio.Environments.Services.Data.Contracts;
    using ProcessingTools.Bio.Environments.Services.Data.Models;
    using ProcessingTools.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Extensions.Linq;

    public class EnvoTermsDataService : IEnvoTermsDataService
    {
        private IBioEnvironmentsRepository<EnvoName> repository;

        public EnvoTermsDataService(IBioEnvironmentsRepository<EnvoName> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public async Task<IQueryable<EnvoTermServiceModel>> All()
        {
            var result = await this.repository.Query
                .Where(n => !n.Content.Contains("ENVO"))
                .OrderByDescending(n => n.Content.Length)
                .Select(n => new EnvoTermServiceModel
                {
                    EntityId = n.EnvoEntityId,
                    Content = n.Content,
                    EnvoId = n.EnvoEntity.EnvoId
                })
                .ToListAsync();

            return result.AsQueryable();
        }

        public async Task<IQueryable<EnvoTermServiceModel>> Get(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            var result = await this.repository.Query
                .Where(n => !n.Content.Contains("ENVO"))
                .OrderByDescending(n => n.Content.Length)
                .Skip(skip)
                .Take(take)
                .Select(n => new EnvoTermServiceModel
                {
                    EntityId = n.EnvoEntityId,
                    Content = n.Content,
                    EnvoId = n.EnvoEntity.EnvoId
                })
                .ToListAsync();

            return result.AsQueryable();
        }
    }
}
