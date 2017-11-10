namespace ProcessingTools.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.DataResources.Data.Entity.Contracts.Repositories;
    using ProcessingTools.DataResources.Data.Entity.Models;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Resources;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Services.Data.Contracts;

    public class InstitutionsDataService : AbstractMultiDataServiceAsync<Institution, IInstitution, IFilter>, IInstitutionsDataService
    {
        public InstitutionsDataService(IResourcesRepository<Institution> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<Institution, IInstitution>> MapEntityToModel => e => new ProcessingTools.Services.Models.Data.Resources.Institution
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<IInstitution, Institution>> MapModelToEntity => m => new DataResources.Data.Entity.Models.Institution
        {
            Id = m.Id,
            Name = m.Name
        };
    }
}
