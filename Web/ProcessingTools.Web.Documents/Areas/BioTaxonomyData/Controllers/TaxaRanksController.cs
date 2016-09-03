namespace ProcessingTools.Web.Documents.Areas.BioTaxonomyData.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Models;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Documents.Extensions;

    using ViewModels.TaxaRanks;

    public class TaxaRanksController : Controller
    {
        private readonly ITaxonRankDataService service;

        public TaxaRanksController(ITaxonRankDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        // GET: /Data/Bio/Taxonomy/TaxaRanks
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost, ActionName(nameof(TaxaRanksController.Index))]
        public async Task<ActionResult> IndexPost(TaxaRanksViewModel viewModel)
        {
            if (viewModel == null || !this.ModelState.IsValid)
            {
                throw new ArgumentException(nameof(viewModel));
            }

            var taxa = viewModel.Taxa
                .Select(i => new TaxonRankServiceModel
                {
                    ScientificName = i.TaxonName,
                    Rank = i.Rank.MapTaxonRankStringToTaxonRankType()
                })
                .ToArray();

            await this.service.Add(taxa);

            return this.RedirectToAction(nameof(this.Index));
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
    }
}
