namespace ProcessingTools.Web.ApiControllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ProcessingTools.Contracts;
    using ProcessingTools.Web.Contracts.Services.Geo;

    [Authorize]
    public class CountriesController : ApiController
    {
        private readonly ICountriesApiService service;
        private readonly ILogger logger;

        public CountriesController(ICountriesApiService service, ILogger logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger;
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
                this.logger?.Log(exception: ex, message: nameof(this.Get));
                return this.InternalServerError();
            }
        }
    }
}
