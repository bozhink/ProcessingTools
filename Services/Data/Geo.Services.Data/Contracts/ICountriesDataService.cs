namespace ProcessingTools.Geo.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Countries.Contracts;

    public interface ICountriesDataService
    {
        Task<IEnumerable<ICountryListableServiceModel>> All();
    }
}
