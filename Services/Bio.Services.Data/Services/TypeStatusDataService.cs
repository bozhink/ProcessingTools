namespace ProcessingTools.Bio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Contracts;
    using Models;

    using ProcessingTools.Bio.Data.Models;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class TypeStatusDataService : MultiDataServiceWithRepositoryFactory<TypeStatus, TypeStatusServiceModel>, ITypeStatusDataService
    {
        public TypeStatusDataService(IBioDataRepository<TypeStatus> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<TypeStatus, IEnumerable<TypeStatusServiceModel>>> MapDbModelToServiceModel => e => new TypeStatusServiceModel[]
        {
            new TypeStatusServiceModel
            {
                Id = e.Id,
                Name = e.Name
            }
        };

        protected override Expression<Func<TypeStatusServiceModel, IEnumerable<TypeStatus>>> MapServiceModelToDbModel => m => new TypeStatus[]
        {
            new TypeStatus
            {
                Id = m.Id,
                Name = m.Name
            }
        };

        protected override Expression<Func<TypeStatus, object>> SortExpression => t => t.Name;
    }
}
