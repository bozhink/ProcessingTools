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

    public class GeoNamesDataService : MultiDataServiceWithRepositoryFactory<GeoName, GeoNameServiceModel>, IGeoNamesDataService
    {
        public GeoNamesDataService(IGeoDataRepository<GeoName> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<GeoName, IEnumerable<GeoNameServiceModel>>> MapDbModelToServiceModel => e => new GeoNameServiceModel[]
        {
            new GeoNameServiceModel
            {
                Id = e.Id,
                Name = e.Name
            }
        };

        protected override Expression<Func<GeoNameServiceModel, IEnumerable<GeoName>>> MapServiceModelToDbModel => m => new GeoName[]
        {
            new GeoName
            {
                Id = m.Id,
                Name = m.Name
            }
        };

        protected override Expression<Func<GeoName, object>> SortExpression => g => g.Name;
    }
}
