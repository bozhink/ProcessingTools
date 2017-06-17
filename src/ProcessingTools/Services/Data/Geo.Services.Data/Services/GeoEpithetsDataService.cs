namespace ProcessingTools.Geo.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models;
    using ProcessingTools.Services.Abstractions;

    public class GeoEpithetsDataService : AbstractDataServiceWithRepository<GeoEpithet, GeoEpithetServiceModel>, IGeoEpithetsDataService
    {
        public GeoEpithetsDataService(IGeoDataRepository<GeoEpithet> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<GeoEpithet, GeoEpithetServiceModel>> MapDbModelToServiceModel => e => new GeoEpithetServiceModel
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<GeoEpithetServiceModel, GeoEpithet>> MapServiceModelToDbModel => m => new GeoEpithet
        {
            Id = m.Id,
            Name = m.Name
        };

        protected override Expression<Func<GeoEpithet, object>> SortExpression => g => g.Name;
    }
}
