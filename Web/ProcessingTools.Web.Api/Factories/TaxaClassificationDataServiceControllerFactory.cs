namespace ProcessingTools.Web.Api.Factories
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Bio.Taxonomy.Contracts;
    using Bio.Taxonomy.Services.Data.Contracts;
    using Models.TaxonomyDataServices;

    public abstract class TaxaClassificationDataServiceControllerFactory : ApiController
    {
        protected ITaxaDataService<ITaxonClassification> Service { get; set; }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get(string id)
        {
            var result = this.Service.Resolve(id)?.Select(AutoMapperConfig.Mapper.Map<TaxonClassificationResponseModel>).ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}