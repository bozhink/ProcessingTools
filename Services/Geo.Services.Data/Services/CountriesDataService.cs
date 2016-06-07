namespace ProcessingTools.Geo.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;

    using Contracts;
    using Models;

    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class CountriesDataService : SimpleDataServiceWithRepositoryProviderFactory<Country, CountryServiceModel>, ICountriesDataService
    {
        public CountriesDataService(IGeoDataRepositoryProvider<Country> repositoryProvider)
            : base(repositoryProvider)
        {
        }

        protected override Expression<Func<Country, CountryServiceModel>> MapDbModelToServiceModel => e => new CountryServiceModel
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<CountryServiceModel, Country>> MapServiceModelToDbModel => m => new Country
        {
            Id = m.Id,
            Name = m.Name
        };

        protected override Expression<Func<Country, object>> SortExpression => c => c.Name;
    }
}
