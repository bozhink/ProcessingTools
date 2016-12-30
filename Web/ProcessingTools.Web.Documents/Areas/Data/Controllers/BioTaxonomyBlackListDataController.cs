namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Common;
    using ProcessingTools.Net.Constants;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Web.Documents.Areas.Data.Models.BioTaxonomyBlackList;

    [Authorize]
    public class BioTaxonomyBlackListDataController : Controller
    {
        private readonly IBlackListDataService dataService;
        private readonly IBlackListSearchService searchService;

        public BioTaxonomyBlackListDataController(
            IBlackListDataService dataService,
            IBlackListSearchService searchService)
        {
            if (dataService == null)
            {
                throw new ArgumentNullException(nameof(dataService));
            }

            if (searchService == null)
            {
                throw new ArgumentNullException(nameof(searchService));
            }

            this.dataService = dataService;
            this.searchService = searchService;
        }

        [HttpPost]
        public async Task<JsonResult> Post(BlackListItemsRequestModel viewModel)
        {
            if (viewModel == null || !this.ModelState.IsValid)
            {
                throw new ArgumentException(nameof(viewModel));
            }

            var taxa = viewModel.Items
                .Select(i => i.Content)
                .ToArray();

            await this.dataService.Upsert(taxa);

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

            var foundItems = (await this.searchService.Search(searchString))
                .ToList();

            if (foundItems.Count < 1)
            {
                this.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.GetEmptyJsonResult();
            }

            var responseItems = foundItems.Select(i => new BlackListItemResponseModel
            {
                Content = i
            });

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.GetJsonResult(
                new SearchResposeModel(
                    responseItems.ToArray()));
        }

        private JsonResult GetEmptyJsonResult()
        {
            return this.GetJsonResult(null);
        }

        private JsonResult GetJsonResult(object data)
        {
            return new JsonResult
            {
                ContentType = ContentTypeConstants.JsonContentType,
                ContentEncoding = Defaults.DefaultEncoding,
                JsonRequestBehavior = JsonRequestBehavior.DenyGet,
                Data = data
            };
        }
    }
}
