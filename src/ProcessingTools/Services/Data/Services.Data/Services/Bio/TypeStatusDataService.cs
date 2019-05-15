namespace ProcessingTools.Bio.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Data.Entity.Bio;
    using ProcessingTools.Data.Models.Entity.Bio;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Services.Contracts.Bio;
    using ProcessingTools.Services.Models.Contracts.Bio;

    /// <summary>
    /// Type-status data service.
    /// </summary>
    public class TypeStatusDataService : AbstractMultiDataServiceAsync<TypeStatus, ITypeStatus, IFilter>, ITypeStatusDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeStatusDataService"/> class.
        /// </summary>
        /// <param name="repository">Type-status repository.</param>
        public TypeStatusDataService(IBioDataRepository<TypeStatus> repository)
            : base(repository)
        {
        }

        /// <inheritdoc/>
        protected override Expression<Func<TypeStatus, ITypeStatus>> MapEntityToModel => e => new ProcessingTools.Services.Models.Data.Bio.TypeStatus
        {
            Id = e.Id,
            Name = e.Name,
        };

        /// <inheritdoc/>
        protected override Expression<Func<ITypeStatus, TypeStatus>> MapModelToEntity => m => new TypeStatus
        {
            Id = m.Id,
            Name = m.Name,
        };
    }
}
