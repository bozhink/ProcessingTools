﻿namespace ProcessingTools.Web.ApiControllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Web.Services.Geo;

    [Authorize]
    public class CountriesController : ApiController
    {
        private readonly ICountriesApiService service;
        private readonly ILogger logger;

        public CountriesController(ICountriesApiService service, ILogger<CountriesController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/Countries
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var data = await this.service.GetAllAsync().ConfigureAwait(false);
                return this.Ok(data);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, nameof(this.Get));
                return this.InternalServerError();
            }
        }
    }
}
