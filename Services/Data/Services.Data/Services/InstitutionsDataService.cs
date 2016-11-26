namespace ProcessingTools.Services.Data
{
    using System;
    using System.Linq.Expressions;
    using Contracts;
    using Models;
    using ProcessingTools.Resources.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Resources.Data.Entity.Models;
    using ProcessingTools.Services.Common.Factories;

    public class InstitutionsDataService : SimpleDataServiceWithRepositoryFactory<Institution, InstitutionServiceModel>, IInstitutionsDataService
    {
        public InstitutionsDataService(IResourcesRepository<Institution> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<Institution, InstitutionServiceModel>> MapDbModelToServiceModel => e => new InstitutionServiceModel
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<InstitutionServiceModel, Institution>> MapServiceModelToDbModel => m => new Institution
        {
            Id = m.Id,
            Name = m.Name
        };

        protected override Expression<Func<Institution, object>> SortExpression => i => i.Name;
    }
}
