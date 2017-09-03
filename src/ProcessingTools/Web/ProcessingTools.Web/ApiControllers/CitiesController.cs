namespace ProcessingTools.Web.ApiControllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Web.Models.Geo.Cities;

    [Authorize]
    public class CitiesController : ApiController
    {
        private readonly ICitiesDataService service;

        public CitiesController(ICitiesDataService service)
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
            var items = await this.service.SelectAsync(null);

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

        // GET: api/Cities/5
        public async Task<CityResponseModel> Get(int id)
        {
            var item = await this.service.GetByIdAsync(id);

            return new CityResponseModel
            {
                Id = item.Id,
                Name = item.Name,
                Country = new CountryResponseModel
                {
                    Id = item.Country.Id,
                    Name = item.Country.Name
                }
            };
        }
    }
}
