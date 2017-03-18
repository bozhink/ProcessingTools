namespace ProcessingTools.Bio.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Bio.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Bio.Data.Entity.Models;
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Services.Common.Abstractions;

    public class MorphologicalEpithetsDataService : AbstractDataServiceWithRepository<MorphologicalEpithet, MorphologicalEpithetServiceModel>, IMorphologicalEpithetsDataService
    {
        public MorphologicalEpithetsDataService(IBioDataRepository<MorphologicalEpithet> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<MorphologicalEpithet, MorphologicalEpithetServiceModel>> MapDbModelToServiceModel => e => new MorphologicalEpithetServiceModel
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<MorphologicalEpithetServiceModel, MorphologicalEpithet>> MapServiceModelToDbModel => m => new MorphologicalEpithet
        {
            Id = m.Id,
            Name = m.Name
        };

        protected override Expression<Func<MorphologicalEpithet, object>> SortExpression => m => m.Name;
    }
}
