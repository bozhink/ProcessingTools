namespace ProcessingTools.Geo.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models;
    using ProcessingTools.Services.Abstractions;

    public class GeoNamesDataService : AbstractMultiDataServiceAsync<GeoName, GeoNameServiceModel, IFilter>, IGeoNamesDataService
    {
        public GeoNamesDataService(IGeoDataRepository<GeoName> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<GeoName, GeoNameServiceModel>> MapEntityToModel => e => new GeoNameServiceModel
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<GeoNameServiceModel, GeoName>> MapModelToEntity => m => new GeoName
        {
            Id = m.Id,
            Name = m.Name
        };
    }
}
