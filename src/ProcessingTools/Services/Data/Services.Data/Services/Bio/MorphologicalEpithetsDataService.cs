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
    /// Morphological-epithets data service.
    /// </summary>
    public class MorphologicalEpithetsDataService : AbstractMultiDataServiceAsync<MorphologicalEpithet, IMorphologicalEpithet, IFilter>, IMorphologicalEpithetsDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MorphologicalEpithetsDataService"/> class.
        /// </summary>
        /// <param name="repository">Morphological-epithets repository.</param>
        public MorphologicalEpithetsDataService(IBioDataRepository<MorphologicalEpithet> repository)
            : base(repository)
        {
        }

        /// <inheritdoc/>
        protected override Expression<Func<MorphologicalEpithet, IMorphologicalEpithet>> MapEntityToModel => e => new ProcessingTools.Services.Models.Data.Bio.MorphologicalEpithet
        {
            Id = e.Id,
            Name = e.Name,
        };

        /// <inheritdoc/>
        protected override Expression<Func<IMorphologicalEpithet, MorphologicalEpithet>> MapModelToEntity => m => new MorphologicalEpithet
        {
            Id = m.Id,
            Name = m.Name,
        };
    }
}
