namespace ProcessingTools.Web.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using AutoMapper;
    using ProcessingTools.Bio.Environments.Services.Data.Contracts;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Bio;
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

        public async Task<IHttpActionResult> Get(int skip = PagingConstants.DefaultSkip, int take = PagingConstants.DefaultTake)
        {
            try
            {
                var result = await this.service.Get(skip, take).ConfigureAwait(false);
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
