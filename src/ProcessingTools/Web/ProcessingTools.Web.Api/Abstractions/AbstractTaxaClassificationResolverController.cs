namespace ProcessingTools.Web.Api.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Models.TaxonClassifications;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    public abstract class AbstractTaxaClassificationResolverController : ApiController
    {
        private readonly ITaxaClassificationResolver resolver;

        public AbstractTaxaClassificationResolverController(ITaxaClassificationResolver resolver)
        {
            if (resolver == null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            this.resolver = resolver;
        }

        [EnableCors("*", "*", "*")]
        public async Task<IHttpActionResult> Get(string id)
        {
            var result = (await this.resolver.Resolve(id))
                .Select(AutoMapperConfig.Mapper.Map<TaxonClassificationResponseModel>)
                .ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}
