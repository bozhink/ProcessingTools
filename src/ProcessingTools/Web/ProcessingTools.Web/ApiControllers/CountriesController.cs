namespace ProcessingTools.Web.ApiControllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ProcessingTools.Geo.Services.Data.Contracts.Services;
    using ProcessingTools.Web.Models.Countries;

    public class CountriesController : ApiController
    {
        private readonly ICountriesSelectableDataService service;

        public CountriesController(ICountriesSelectableDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        // GET: api/Countries
        public async Task<IEnumerable<CountryResponseModel>> Get()
        {
            var items = await this.service.Select();

            return items.Select(c => new CountryResponseModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToArray();
        }
    }
}
