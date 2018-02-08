namespace ProcessingTools.Bio.Environments.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Environments.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Bio.Environments.Data.Entity.Models;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Services.Data.Bio.Environments;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Models.Contracts.Services.Data.Bio.Environments;
    using ProcessingTools.Services.Models.Data.Bio.Environments;

    public class EnvoTermsDataService : IEnvoTermsDataService
    {
        private readonly IBioEnvironmentsRepository<EnvoName> repository;

        public EnvoTermsDataService(IBioEnvironmentsRepository<EnvoName> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<IEnvoTerm[]> AllAsync()
        {
            return Task.Run(() =>
            {
                var data = this.repository.Query
                    .Where(n => !n.Content.Contains("ENVO"))
                    .OrderByDescending(n => n.Content.Length)
                    .Select(n => new EnvoTerm
                    {
                        EntityId = n.EnvoEntityId,
                        Content = n.Content,
                        EnvoId = n.EnvoEntity.EnvoId
                    })
                    .ToArray<IEnvoTerm>();

                return data;
            });
        }

        public Task<IEnvoTerm[]> GetAsync(int skip, int take)
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
                var data = this.repository.Query
                    .Where(n => !n.Content.Contains("ENVO"))
                    .OrderByDescending(n => n.Content.Length)
                    .Skip(skip)
                    .Take(take)
                    .Select(n => new EnvoTerm
                    {
                        EntityId = n.EnvoEntityId,
                        Content = n.Content,
                        EnvoId = n.EnvoEntity.EnvoId
                    })
                    .ToArray<IEnvoTerm>();

                return data;
            });
        }
    }
}
