namespace ProcessingTools.Bio.Services.Data
{
    using System;
    using System.Linq.Expressions;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Data.Models;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class MorphologicalEpithetsDataService : SimpleDataServiceWithRepositoryFactory<MorphologicalEpithet, MorphologicalEpithetServiceModel>, IMorphologicalEpithetsDataService
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
