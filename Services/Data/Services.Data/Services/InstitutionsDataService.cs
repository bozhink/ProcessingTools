namespace ProcessingTools.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using Contracts;
    using Contracts.Models;
    using Models;
    using ProcessingTools.DataResources.Data.Entity.Contracts.Repositories;
    using ProcessingTools.DataResources.Data.Entity.Models;
    using ProcessingTools.Services.Common.Factories;

    public class InstitutionsDataService : SimpleDataServiceWithRepositoryFactory<Institution, IInstitution>, IInstitutionsDataService
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
