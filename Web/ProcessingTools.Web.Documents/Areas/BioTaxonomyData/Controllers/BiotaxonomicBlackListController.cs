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
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Net.Constants;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Documents.Extensions;

    using ViewModels.BiotaxonomicBlackList;

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

        // GET: /Data/Bio/Taxonomy/BiotaxonomicBlackList
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost, ActionName(nameof(TaxaRanksController.Index))]
        public async Task<ActionResult> IndexPost(BlackListItemsViewModel viewModel)
        {
            if (viewModel == null || !this.ModelState.IsValid)
            {
                throw new ArgumentException(nameof(viewModel));
            }

            var taxa = viewModel.Items
                .Select(i => i.Content)
                .ToArray();

            await this.dataService.Add(taxa);

            return this.RedirectToAction(nameof(this.Index));
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

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is EntityNotFoundException)
            {
                filterContext.Result = this.DefaultNotFoundView(
                    InstanceNames.TaxaRanksControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidUserIdException)
            {
                filterContext.Result = this.InvalidUserIdErrorView(
                    InstanceNames.TaxaRanksControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidIdException)
            {
                filterContext.Result = this.InvalidIdErrorView(
                    InstanceNames.TaxaRanksControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidPageNumberException)
            {
                filterContext.Result = this.InvalidPageNumberErrorView(
                    InstanceNames.TaxaRanksControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidItemsPerPageException)
            {
                filterContext.Result = this.InvalidNumberOfItemsPerPageErrorView(
                    InstanceNames.TaxaRanksControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is ArgumentException)
            {
                filterContext.Result = this.BadRequestErrorView(
                    InstanceNames.TaxaRanksControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else
            {
                filterContext.Result = this.DefaultErrorView(
                    InstanceNames.TaxaRanksControllerInstanceName,
                    filterContext.Exception.Message);
            }

            filterContext.ExceptionHandled = true;
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
