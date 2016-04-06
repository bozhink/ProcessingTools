namespace ProcessingTools.Geo.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models;

    using ProcessingTools.Geo.Data.Repositories.Contracts;
    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Services.Common.Factories;

    public class CountriesDataService : GenericRepositoryDataServiceFactory<Country, CountryServiceModel>, ICountriesDataService
    {
        public CountriesDataService(IGeoDataRepository<Country> repository)
            : base(repository)
        {
        }

        protected override IEnumerable<Country> MapServiceModelToDbModel(params CountryServiceModel[] models)
        {
            if (models == null)
            {
                throw new ArgumentNullException(nameof(models));
            }

            var result = models.Select(m => new Country
            {
                Name = m.Name
            });

            return new HashSet<Country>(result);
        }

        protected override IEnumerable<CountryServiceModel> MapDbModelToServiceModel(params Country[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            var result = entities.Select(e => new CountryServiceModel
            {
                Id = e.Id,
                Name = e.Name
            });

            return new HashSet<CountryServiceModel>(result);
        }
}
}
