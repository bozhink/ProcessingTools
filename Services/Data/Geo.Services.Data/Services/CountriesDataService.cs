namespace ProcessingTools.Geo.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models.Countries;
    using Models.Countries.Contracts;

    using ProcessingTools.Extensions;
    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;

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
