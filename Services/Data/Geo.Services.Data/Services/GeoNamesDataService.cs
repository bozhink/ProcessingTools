namespace ProcessingTools.Geo.Services.Data
{
    using System;
    using System.Linq.Expressions;

    using Contracts;
    using Models;

    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Services.Common.Factories;

    public class GeoNamesDataService : SimpleDataServiceWithRepositoryFactory<GeoName, GeoNameServiceModel>, IGeoNamesDataService
    {
        public GeoNamesDataService(IGeoDataRepository<GeoName> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<GeoName, GeoNameServiceModel>> MapDbModelToServiceModel => e => new GeoNameServiceModel
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<GeoNameServiceModel, GeoName>> MapServiceModelToDbModel => m => new GeoName
        {
            Id = m.Id,
            Name = m.Name
        };

        protected override Expression<Func<GeoName, object>> SortExpression => g => g.Name;
    }
}
