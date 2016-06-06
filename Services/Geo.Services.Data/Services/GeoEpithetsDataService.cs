namespace ProcessingTools.Geo.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Contracts;
    using Models;

    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class GeoEpithetsDataService : MultiDataServiceWithRepositoryFactory<GeoEpithet, GeoEpithetServiceModel>, IGeoEpithetsDataService
    {
        public GeoEpithetsDataService(IGeoDataRepository<GeoEpithet> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<GeoEpithet, IEnumerable<GeoEpithetServiceModel>>> MapDbModelToServiceModel => e => new GeoEpithetServiceModel[]
        {
            new GeoEpithetServiceModel
            {
                Id = e.Id,
                Name = e.Name
            }
        };

        protected override Expression<Func<GeoEpithetServiceModel, IEnumerable<GeoEpithet>>> MapServiceModelToDbModel => m => new GeoEpithet[]
        {
            new GeoEpithet
            {
                Id = m.Id,
                Name = m.Name
            }
        };

        protected override Expression<Func<GeoEpithet, object>> SortExpression => g => g.Name;
    }
}
