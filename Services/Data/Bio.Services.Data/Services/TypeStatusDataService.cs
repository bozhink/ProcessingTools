namespace ProcessingTools.Bio.Services.Data
{
    using System;
    using System.Linq.Expressions;
    using Contracts;
    using Models;

    using ProcessingTools.Bio.Data.Entity.Models;
    using ProcessingTools.Bio.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Services.Common.Factories;

    public class TypeStatusDataService : SimpleDataServiceWithRepositoryFactory<TypeStatus, TypeStatusServiceModel>, ITypeStatusDataService
    {
        public TypeStatusDataService(IBioDataRepository<TypeStatus> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<TypeStatus, TypeStatusServiceModel>> MapDbModelToServiceModel => e => new TypeStatusServiceModel
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<TypeStatusServiceModel, TypeStatus>> MapServiceModelToDbModel => m => new TypeStatus
        {
            Id = m.Id,
            Name = m.Name
        };

        protected override Expression<Func<TypeStatus, object>> SortExpression => t => t.Name;
    }
}
