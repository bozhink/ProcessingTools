namespace ProcessingTools.Web.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;
    using Bio.Taxonomy.Services.Data.Contracts;
    using Models.TaxonomyDataServices;

    public class AphiaController : ApiController
    {
        private IAphiaTaxaClassificationDataService aphiaService;

        public AphiaController(IAphiaTaxaClassificationDataService aphiaService)
        {
            this.aphiaService = aphiaService;
        }

        [Route("api/aphia/{scientificName}")]
        public IHttpActionResult Get(string scientificName)
        {
            var result = this.aphiaService.Resolve(scientificName)?.ProjectTo<TaxonClassificationResponseModel>().ToList();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }
    }
}