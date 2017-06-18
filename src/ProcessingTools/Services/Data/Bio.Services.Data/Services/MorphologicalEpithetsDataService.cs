namespace ProcessingTools.Bio.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Bio.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Bio.Data.Entity.Models;
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Services.Abstractions;

    public class MorphologicalEpithetsDataService : AbstractMultiDataServiceAsync<MorphologicalEpithet, MorphologicalEpithetServiceModel, IFilter>, IMorphologicalEpithetsDataService
    {
        public MorphologicalEpithetsDataService(IBioDataRepository<MorphologicalEpithet> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<MorphologicalEpithet, MorphologicalEpithetServiceModel>> MapEntityToModel => e => new MorphologicalEpithetServiceModel
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<MorphologicalEpithetServiceModel, MorphologicalEpithet>> MapModelToEntity => m => new MorphologicalEpithet
        {
            Id = m.Id,
            Name = m.Name
        };
    }
}
