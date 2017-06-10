namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Models.Bio.Taxonomy;
    using ProcessingTools.Web.Documents.Areas.Data.Models.BioTaxonomyTaxaRanks;

    [Authorize]
    public class BioTaxonomyTaxaRanksDataController : Controller
    {
        private readonly ITaxonRankDataService dataService;
        private readonly ITaxonRankSearchService searchService;

        public BioTaxonomyTaxaRanksDataController(
            ITaxonRankDataService dataService,
            ITaxonRankSearchService searchService)
        {
            this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            this.searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
        }

        [HttpPost]
        public async Task<JsonResult> Post(TaxaRanksRequestModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                throw new ArgumentException(nameof(model));
            }

            var taxa = model.Taxa
                .Select(i => new TaxonRankServiceModel
                {
                    ScientificName = i.TaxonName,
                    Rank = i.Rank.MapTaxonRankStringToTaxonRankType()
                })
                .ToArray();

            await this.dataService.Add(taxa);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.GetEmptyJsonResult();
        }

        [HttpPost]
        public async Task<JsonResult> Search(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.GetEmptyJsonResult();
            }

            var foundTaxa = await this.searchService.Search(searchString);
            if (foundTaxa == null || foundTaxa.Count() < 1)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.GetEmptyJsonResult();
            }

            var responseTaxa = foundTaxa.Select(t => new TaxonRankResponseModel
            {
                TaxonName = t.ScientificName,
                Rank = t.Rank.MapTaxonRankTypeToTaxonRankString()
            });

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.GetJsonResult(
                new SearchResposeModel(
                    responseTaxa.ToArray()));
        }

        private JsonResult GetEmptyJsonResult()
        {
            return this.GetJsonResult(null);
        }

        private JsonResult GetJsonResult(object data)
        {
            return new JsonResult
            {
                ContentType = ContentTypes.Json,
                ContentEncoding = Defaults.Encoding,
                JsonRequestBehavior = JsonRequestBehavior.DenyGet,
                Data = data
            };
        }
    }
}
