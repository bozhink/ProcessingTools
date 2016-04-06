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

    public class MorphologicalEpithetsDataService : GenericRepositoryDataServiceFactory<MorphologicalEpithet, MorphologicalEpithetServiceModel>, IMorphologicalEpithetsDataService
    {
        public MorphologicalEpithetsDataService(IBioDataRepository<MorphologicalEpithet> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<MorphologicalEpithet, IEnumerable<MorphologicalEpithetServiceModel>>> MapDbModelToServiceModel => e => new MorphologicalEpithetServiceModel[]
        {
            new MorphologicalEpithetServiceModel
            {
                Id = e.Id,
                Name = e.Name
            }
        };

        protected override Expression<Func<MorphologicalEpithetServiceModel, IEnumerable<MorphologicalEpithet>>> MapServiceModelToDbModel => m => new MorphologicalEpithet[]
        {
            new MorphologicalEpithet
            {
                Id = m.Id,
                Name = m.Name
            }
        };
    }
}
