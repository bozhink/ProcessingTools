namespace ProcessingTools.Web.Api.Factories
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper.QueryableExtensions;
    using Bio.Taxonomy.Contracts;
    using Bio.Taxonomy.Services.Data.Contracts;
    using Models.TaxonomyDataServices;

    public abstract class TaxaClassificationDataServiceControllerFactory : ApiController
    {
        protected ITaxaDataService<ITaxonClassification> service;

        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get(string id)
        {
            var result = this.service.Resolve(id)?.ProjectTo<TaxonClassificationResponseModel>().ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}