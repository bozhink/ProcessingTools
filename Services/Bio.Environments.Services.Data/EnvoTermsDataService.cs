namespace ProcessingTools.Bio.Environments.Services.Data
{
    using System;
    using System.Linq;

    using Contracts;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Bio.Environments.Data.Models;
    using ProcessingTools.Bio.Environments.Data.Repositories;

    using ProcessingTools.Common.Constants;

    public class EnvoTermsDataService : IEnvoTermsDataService
    {
        private IBioEnvironmentsRepository<EnvoName> envoNames;

        public EnvoTermsDataService(IBioEnvironmentsRepository<EnvoName> envoNames)
        {
            this.envoNames = envoNames;
        }

        public IQueryable<IEnvoTerm> All()
        {
            var result = this.envoNames.All()
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
                throw new ArgumentException("Skip schould be non-negative.", "skip");
            }

            if (1 > take || take > DefaultPagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new ArgumentException($"Take schould be non-negative and less than {DefaultPagingConstants.MaximalItemsPerPageAllowed}.", "take");
            }

            var result = this.envoNames.All()
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
