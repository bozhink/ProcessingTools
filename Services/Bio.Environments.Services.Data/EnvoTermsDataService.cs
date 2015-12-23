namespace ProcessingTools.Bio.Environments.Services.Data
{
    using System.Linq;

    using Common.Exceptions;
    using Contracts;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Bio.Environments.Data.Models;
    using ProcessingTools.Bio.Environments.Data.Repositories;
    using ProcessingTools.Common.Constants;

    public class EnvoTermsDataService : IEnvoTermsDataService
    {
        private IBioEnvironmentsRepository<EnvoName> repository;

        public EnvoTermsDataService(IBioEnvironmentsRepository<EnvoName> repository)
        {
            this.repository = repository;
        }

        public IQueryable<IEnvoTerm> All()
        {
            var result = this.repository.All()
                .Where(n => !n.Content.Contains("ENVO"))
                .OrderByDescending(n => n.Content.Length)
                .Select(n => new EnvoTermDataServiceResponseModel
                {
                    EntityId = n.EnvoEntityId,
                    Content = n.Content,
                    EnvoId = n.EnvoEntity.EnvoId
                });

            return result;
        }

        public IQueryable<IEnvoTerm> Get(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            var result = this.repository.All()
                .Where(n => !n.Content.Contains("ENVO"))
                .OrderByDescending(n => n.Content.Length)
                .Skip(skip)
                .Take(take)
                .Select(n => new EnvoTermDataServiceResponseModel
                {
                    EntityId = n.EnvoEntityId,
                    Content = n.Content,
                    EnvoId = n.EnvoEntity.EnvoId
                });

            return result;
        }
    }
}
