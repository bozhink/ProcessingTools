namespace ProcessingTools.Web.ApiControllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ProcessingTools.Geo.Services.Data.Contracts.Services;
    using ProcessingTools.Web.Models.Cities;

    public class CitiesController : ApiController
    {
        private readonly ICitiesSelectableDataService service;

        public CitiesController(ICitiesSelectableDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        // GET: api/Cities
        public async Task<IEnumerable<CityResponseModel>> Get()
        {
            var items = await this.service.Select();

            return items.Select(c => new CityResponseModel
            {
                Id = c.Id,
                Name = c.Name,
                Country = new CountryResponseModel
                {
                    Id = c.Country.Id,
                    Name = c.Country.Name
                }
            })
            .ToArray();
        }
    }
}
