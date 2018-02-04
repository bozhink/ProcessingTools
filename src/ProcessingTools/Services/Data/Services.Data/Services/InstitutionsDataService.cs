namespace ProcessingTools.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Resources;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.DataResources.Data.Entity.Contracts.Repositories;
    using ProcessingTools.DataResources.Data.Entity.Models;
    using ProcessingTools.Services.Abstractions;

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
