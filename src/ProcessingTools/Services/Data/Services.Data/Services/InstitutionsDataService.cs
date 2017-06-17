namespace ProcessingTools.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.DataResources.Data.Entity.Contracts.Repositories;
    using ProcessingTools.DataResources.Data.Entity.Models;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Contracts.Models;
    using ProcessingTools.Services.Data.Models;

    public class InstitutionsDataService : AbstractDataServiceWithRepository<Institution, IInstitution, IFilter>, IInstitutionsDataService
    {
        public InstitutionsDataService(IResourcesRepository<Institution> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<Institution, IInstitution>> MapDbModelToServiceModel => e => new InstitutionServiceModel
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<IInstitution, Institution>> MapServiceModelToDbModel => m => new Institution
        {
            Id = m.Id,
            Name = m.Name
        };

        protected override Expression<Func<Institution, object>> SortExpression => i => i.Name;
    }
}
