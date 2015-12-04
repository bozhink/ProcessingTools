namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Bio.Taxonomy.Services.Data.Contracts;
    using Models.TaxonomyDataServices;
    using System.Linq;
    using System.Web.Http;

    public class GbifController : ApiController
    {
        private IGbifTaxaClassificationDataService service;

        public GbifController(IGbifTaxaClassificationDataService gbifSevice)
        {
            this.service = gbifSevice;
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