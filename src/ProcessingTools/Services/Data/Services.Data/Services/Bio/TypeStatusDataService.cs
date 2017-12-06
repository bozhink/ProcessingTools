﻿namespace ProcessingTools.Bio.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Bio.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Bio.Data.Entity.Models;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Contracts.Services.Data.Bio;
    using ProcessingTools.Contracts.Services.Models.Data.Bio;

    public class TypeStatusDataService : AbstractMultiDataServiceAsync<TypeStatus, ITypeStatus, IFilter>, ITypeStatusDataService
    {
        public TypeStatusDataService(IBioDataRepository<TypeStatus> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<TypeStatus, ITypeStatus>> MapEntityToModel => e => new ProcessingTools.Services.Models.Data.Bio.TypeStatus
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<ITypeStatus, TypeStatus>> MapModelToEntity => m => new TypeStatus
        {
            Id = m.Id,
            Name = m.Name
        };
    }
}
