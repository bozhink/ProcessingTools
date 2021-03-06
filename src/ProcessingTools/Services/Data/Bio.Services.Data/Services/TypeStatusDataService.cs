﻿namespace ProcessingTools.Bio.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Bio.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Bio.Data.Entity.Models;
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Services.Abstractions;

    public class TypeStatusDataService : AbstractMultiDataServiceAsync<TypeStatus, TypeStatusServiceModel, IFilter>, ITypeStatusDataService
    {
        public TypeStatusDataService(IBioDataRepository<TypeStatus> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<TypeStatus, TypeStatusServiceModel>> MapEntityToModel => e => new TypeStatusServiceModel
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<TypeStatusServiceModel, TypeStatus>> MapModelToEntity => m => new TypeStatus
        {
            Id = m.Id,
            Name = m.Name
        };
    }
}
