namespace ProcessingTools.Web.Documents.Areas.BioTaxonomyData.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Models.BiotaxonomicBlackList;

    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Common;
    using ProcessingTools.Net.Constants;
    using ProcessingTools.Web.Documents.Extensions;

    [Authorize]
    public class BiotaxonomicBlackListController : Controller
    {
        private readonly IBiotaxonomicBlackListDataService dataService;
        private readonly IBiotaxonomicBlackListIterableDataService searchService;

        public BiotaxonomicBlackListController(IBiotaxonomicBlackListDataService dataService, IBiotaxonomicBlackListIterableDataService searchService)
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

        [HttpGet]
        public ActionResult Help()
        {
            return this.View();
        }

        // GET: /Data/Bio/Taxonomy/BiotaxonomicBlackList
        [HttpGet]
        public ActionResult Index()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        [HttpPost, ActionName(nameof(Index))]
        public async Task<JsonResult> IndexPost(BlackListItemsRequestModel viewModel)
        {
            if (viewModel == null || !this.ModelState.IsValid)
            {
                throw new ArgumentException(nameof(viewModel));
            }

            var taxa = viewModel.Items
                .Select(i => i.Content)
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

            var foundItems = (await this.searchService.All())
                .Where(i => i.Contains(searchString))
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

        protected override void HandleUnknownAction(string actionName)
        {
            this.IvalidActionErrorView(actionName).ExecuteResult(this.ControllerContext);
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
