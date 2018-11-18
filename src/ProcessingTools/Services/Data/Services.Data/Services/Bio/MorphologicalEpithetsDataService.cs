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

    public class MorphologicalEpithetsDataService : AbstractMultiDataServiceAsync<MorphologicalEpithet, IMorphologicalEpithet, IFilter>, IMorphologicalEpithetsDataService
    {
        public MorphologicalEpithetsDataService(IBioDataRepository<MorphologicalEpithet> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<MorphologicalEpithet, IMorphologicalEpithet>> MapEntityToModel => e => new ProcessingTools.Services.Models.Data.Bio.MorphologicalEpithet
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<IMorphologicalEpithet, MorphologicalEpithet>> MapModelToEntity => m => new MorphologicalEpithet
        {
            Id = m.Id,
            Name = m.Name
        };
    }
}
