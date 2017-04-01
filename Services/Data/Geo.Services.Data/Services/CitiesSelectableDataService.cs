namespace ProcessingTools.Geo.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Abstractions.Services;
    using ProcessingTools.Geo.Services.Data.Contracts.Models;
    using ProcessingTools.Geo.Services.Data.Contracts.Services;
    using ProcessingTools.Geo.Services.Data.Models;

    public class CitiesSelectableDataService : AbstractSelectableDataService<ICityListableModel, City>, ICitiesSelectableDataService
    {
        public CitiesSelectableDataService(IGeoDataRepository<City> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<City, ICityListableModel>> MapDataModelToServiceModel => c => new CityListableModel
        {
            Id = c.Id,
            Name = c.Name,
            Country = new CountryListableModel
            {
                Id = c.Country.Id,
                Name = c.Country.Name
            }
        };
    }
}
