namespace ProcessingTools.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;
    using ProcessingTools.Data.Entity.DataResources;
    using ProcessingTools.Data.Models.Entity.DataResources;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Services.Contracts.Resources;

    /// <summary>
    /// Institutions data service.
    /// </summary>
    public class InstitutionsDataService : AbstractMultiDataServiceAsync<Institution, IInstitution, IFilter>, IInstitutionsDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstitutionsDataService"/> class.
        /// </summary>
        /// <param name="repository">Institutions repository.</param>
        public InstitutionsDataService(IResourcesRepository<Institution> repository)
            : base(repository)
        {
        }

        /// <inheritdoc/>
        protected override Expression<Func<Institution, IInstitution>> MapEntityToModel => e => new ProcessingTools.Services.Models.Data.Resources.Institution
        {
            Id = e.Id,
            Name = e.Name,
        };

        /// <inheritdoc/>
        protected override Expression<Func<IInstitution, Institution>> MapModelToEntity => m => new Institution
        {
            Id = m.Id,
            Name = m.Name,
        };
    }
}
