namespace ProcessingTools.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Contracts;
    using Models;

    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class InstitutionsDataService : MultiDataServiceWithRepositoryFactory<Institution, InstitutionServiceModel>, IInstitutionsDataService
    {
        public InstitutionsDataService(IDataRepository<Institution> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<Institution, IEnumerable<InstitutionServiceModel>>> MapDbModelToServiceModel => e => new InstitutionServiceModel[]
        {
            new InstitutionServiceModel
            {
                Id = e.Id,
                Name = e.Name
            }
        };

        protected override Expression<Func<InstitutionServiceModel, IEnumerable<Institution>>> MapServiceModelToDbModel => m => new Institution[]
        {
            new Institution
            {
                Id = m.Id,
                Name = m.Name
            }
        };

        protected override Expression<Func<Institution, object>> SortExpression => i => i.Name;
    }
}
