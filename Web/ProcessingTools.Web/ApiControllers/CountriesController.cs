using ProcessingTools.Geo.Services.Data.Contracts.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ProcessingTools.Web.Models.Countries;
using System.Web.Http;

namespace ProcessingTools.Web.ApiControllers
{
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
