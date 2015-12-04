namespace ProcessingTools.Web.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;
    using Bio.Taxonomy.Services.Data.Contracts;
    using Models.TaxonomyDataServices;

    public class GbifController : ApiController
    {
        private IGbifTaxaClassificationDataService service;

        public GbifController(IGbifTaxaClassificationDataService service)
        {
            this.service = service;
        }

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