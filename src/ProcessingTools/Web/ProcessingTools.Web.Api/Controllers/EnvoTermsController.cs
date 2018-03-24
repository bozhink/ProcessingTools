namespace ProcessingTools.Web.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Bio;
    using ProcessingTools.Services.Contracts.Bio.Environments;
    using ProcessingTools.Web.Models.Bio.EnvoTerms;

    public class EnvoTermsController : ApiController
    {
        private readonly IEnvoTermsDataService service;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public EnvoTermsController(IEnvoTermsDataService service, ILogger logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger;

            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IEnvoTerm, EnvoTermResponseModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        public async Task<IHttpActionResult> Get(int skip = PaginationConstants.DefaultSkip, int take = PaginationConstants.DefaultTake)
        {
            try
            {
                var result = await this.service.GetAsync(skip, take).ConfigureAwait(false);
                if (result == null)
                {
                    return this.NotFound();
                }

                var data = result.Select(this.mapper.Map<IEnvoTerm, EnvoTermResponseModel>).ToArray();
                return this.Ok(data);
            }
            catch (Exception ex)
            {
                this.logger?.Log(exception: ex, message: string.Empty);
                return this.InternalServerError();
            }
        }
    }
}
