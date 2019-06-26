namespace ProcessingTools.Bio.Environments.Services.Data.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Entity.Bio.Environments;
    using ProcessingTools.Data.Models.Entity.Bio.Environments;
    using ProcessingTools.Services.Contracts.Bio.Environments;
    using ProcessingTools.Services.Models.Contracts.Bio.Environments;
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
        public Task<IEnvoTerm[]> AllAsync()
        {
            return Task.Run(() =>
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
