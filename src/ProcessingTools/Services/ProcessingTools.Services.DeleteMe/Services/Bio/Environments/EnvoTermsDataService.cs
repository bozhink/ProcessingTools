namespace ProcessingTools.Services.Data.Services.Bio.Environments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Contracts.Models.Bio.Environments;
    using ProcessingTools.Contracts.Services.Bio.Environments;
    using ProcessingTools.Data.Entity.Bio.Environments;
    using ProcessingTools.Data.Models.Entity.Bio.Environments;
    using ProcessingTools.Services.Models.Data.Bio.Environments;

    /// <summary>
    /// ENVO terms data service.
    /// </summary>
    public class EnvoTermsDataService : IEnvoTermsDataService
    {
        private readonly IBioEnvironmentsRepository<EnvoName> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvoTermsDataService"/> class.
        /// </summary>
        /// <param name="repository">ENVO names repository.</param>
        public EnvoTermsDataService(IBioEnvironmentsRepository<EnvoName> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public Task<IList<IEnvoTerm>> AllAsync()
        {
            return Task.Run<IList<IEnvoTerm>>(() =>
            {
                var data = this.repository.Query
                    .Where(n => !n.Value.Contains("ENVO"))
                    .OrderByDescending(n => n.Value.Length)
                    .Select(n => new EnvoTerm
                    {
                        EntityId = n.EntityId,
                        Content = n.Value,
                        EnvoId = n.Entity.EnvoId,
                    })
                    .ToArray<IEnvoTerm>();

                return data;
            });
        }

        /// <inheritdoc/>
        public Task<IList<IEnvoTerm>> GetAsync(int skip, int take)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException($"Invalid skip value = {skip}");
            }

            if (take < 1 || take > PaginationConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException($"Invalid take value = {take}");
            }

            return Task.Run<IList<IEnvoTerm>>(() =>
            {
                var data = this.repository.Query
                    .Where(n => !n.Value.Contains("ENVO"))
                    .OrderByDescending(n => n.Value.Length)
                    .Skip(skip)
                    .Take(take)
                    .Select(n => new EnvoTerm
                    {
                        EntityId = n.EntityId,
                        Content = n.Value,
                        EnvoId = n.Entity.EnvoId,
                    })
                    .ToArray<IEnvoTerm>();

                return data;
            });
        }
    }
}
