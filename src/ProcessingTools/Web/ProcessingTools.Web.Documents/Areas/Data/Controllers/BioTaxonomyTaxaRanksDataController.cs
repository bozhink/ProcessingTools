namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;
    using ProcessingTools.Web.Documents.Areas.Data.Models.BioTaxonomyTaxaRanks;

    [Authorize]
    public class BioTaxonomyTaxaRanksDataController : Controller
    {
        private readonly ITaxonRankDataService service;

        public BioTaxonomyTaxaRanksDataController(ITaxonRankDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        public async Task<JsonResult> Post(TaxaRanksRequestModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                throw new ArgumentException(nameof(model));
            }

            var taxa = model.Taxa
                .Select(i => new TaxonRank
                {
                    ScientificName = i.TaxonName,
                    Rank = i.Rank.MapTaxonRankStringToTaxonRankType()
                })
                .ToArray();

            await this.service.InsertAsync(taxa).ConfigureAwait(false);

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

            var foundTaxa = await this.service.SearchAsync(searchString);
            if (foundTaxa == null || !foundTaxa.Any())
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.GetEmptyJsonResult();
            }

            var responseTaxa = foundTaxa.Select(t => new TaxonRankResponseModel
            {
                TaxonName = t.ScientificName,
                Rank = t.Rank.MapTaxonRankTypeToTaxonRankString()
            });

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
