namespace ProcessingTools.Geo.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models.Cities;
    using Models.Cities.Contracts;

    using ProcessingTools.Extensions;
    using ProcessingTools.Geo.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Geo.Data.Entity.Models;

    public class CitiesDataService : ICitiesDataService
    {
        private readonly IGeoDataRepositoryProvider<City> repositoryProvider;

        public CitiesDataService(IGeoDataRepositoryProvider<City> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        public async Task<IEnumerable<ICityListableServiceModel>> All()
        {
            var repository = this.repositoryProvider.Create();

            var result = (await repository.All())
                .Select(c => new CityListableServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList<ICityListableServiceModel>();

            repository.TryDispose();

            return result;
        }
    }
}
