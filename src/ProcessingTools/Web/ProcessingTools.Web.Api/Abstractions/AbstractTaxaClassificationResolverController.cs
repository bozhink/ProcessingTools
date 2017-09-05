namespace ProcessingTools.Web.Api.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Models.Bio.Taxonomy;

    public abstract class AbstractTaxaClassificationResolverController<TResolver> : ApiController
        where TResolver : ITaxaClassificationResolver
    {
        private readonly ITaxaClassificationResolver resolver;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        protected AbstractTaxaClassificationResolverController(ITaxaClassificationResolver resolver, ILogger logger)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            this.logger = logger;

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
                var result = await this.resolver.Resolve(id).ConfigureAwait(false);
                if (result == null)
                {
                    return this.NotFound();
                }

                var data = result.Select(this.mapper.Map<ITaxonClassification, TaxonClassificationResponseModel>).ToList();
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
