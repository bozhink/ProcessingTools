namespace ProcessingTools.Web.Api.Factories
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Bio.Taxonomy.Contracts;
    using Bio.Taxonomy.Services.Data.Contracts;
    using Models.TaxonClassifications;

    public abstract class TaxaClassificationDataServiceControllerFactory : ApiController
    {
        protected ITaxaInformationResolverDataService<ITaxonClassification> Service { get; set; }

        [EnableCors("*", "*", "*")]
        public async Task<IHttpActionResult> Get(string id)
        {
            var result = (await this.Service.Resolve(id))
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