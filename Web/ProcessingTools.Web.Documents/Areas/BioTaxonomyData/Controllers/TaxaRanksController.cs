namespace ProcessingTools.Web.Documents.Areas.BioTaxonomyData.Controllers
{
    using System;
    using System.Web.Mvc;

    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Documents.Extensions;

    public class TaxaRanksController : Controller
    {
        // GET: /Data/Bio/Taxonomy/TaxaRanks/Index
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
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
