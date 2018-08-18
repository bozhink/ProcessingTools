namespace ProcessingTools.Web.Api.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Models.Bio.Taxonomy;

    public abstract class AbstractTaxonClassificationResolverController : ApiController
    {
        private readonly ITaxonClassificationResolver resolver;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        protected AbstractTaxonClassificationResolverController(ITaxonClassificationResolver resolver, ILogger logger)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<ITaxonClassification, TaxonClassificationResponseModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        [EnableCors("*", "*", "*")]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var result = await this.resolver.ResolveAsync(id).ConfigureAwait(false);
                if (result == null)
                {
                    return this.NotFound();
                }

                var data = result.Select(this.mapper.Map<ITaxonClassification, TaxonClassificationResponseModel>).ToList();
                return this.Ok(data);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, string.Empty);
                return this.InternalServerError();
            }
        }
    }
}
