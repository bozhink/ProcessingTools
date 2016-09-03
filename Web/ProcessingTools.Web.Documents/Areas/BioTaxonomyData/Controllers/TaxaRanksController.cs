namespace ProcessingTools.Web.Documents.Areas.BioTaxonomyData.Controllers
{
    using System.Web.Mvc;

    public class TaxaRanksController : Controller
    {
        // GET: /Data/Bio/Taxonomy/TaxaRanks/Index
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
