namespace ProcessingTools.Geo.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Extensions;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models.Countries;
    using ProcessingTools.Geo.Services.Data.Models.Countries.Contracts;

    public class CountriesDataService : ICountriesDataService
    {
        private readonly IGeoDataRepositoryProvider<Country> repositoryProvider;

        public CountriesDataService(IGeoDataRepositoryProvider<Country> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        public async Task<IEnumerable<ICountryListableServiceModel>> All()
        {
            var repository = this.repositoryProvider.Create();

            var result = (await repository.All())
                .Select(c => new CountryListableServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList<ICountryListableServiceModel>();

            repository.TryDispose();

            return result;
        }
    }
}
