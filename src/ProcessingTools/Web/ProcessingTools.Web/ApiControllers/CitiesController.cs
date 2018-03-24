namespace ProcessingTools.Web.ApiControllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Web.Services.Geo;

    [Authorize]
    public class CitiesController : ApiController
    {
        private readonly ICitiesApiService service;
        private readonly ILogger logger;

        public CitiesController(ICitiesApiService service, ILogger logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger;
        }

        // GET: api/Cities
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

        // GET: api/Cities/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var item = await this.service.GetById(id).ConfigureAwait(false);
                return this.Ok(item);
            }
            catch (Exception ex)
            {
                this.logger?.Log(exception: ex, message: nameof(this.Get));
                return this.InternalServerError();
            }
        }
    }
}
