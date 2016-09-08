namespace ProcessingTools.Web.Documents.Areas.Articles.Controllers
{
    using System;
    using System.Web.Mvc;

    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Documents.Extensions;

    public class DocumentsController : Controller
    {
        // GET: /Articles/Documents
        [HttpGet]
        public ActionResult Index()
        {
            return this.RedirectToAction(
                actionName: nameof(FilesController.Index),
                controllerName: ControllerNames.FilesControllerName);
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
                    InstanceNames.DocumentsControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidIdException)
            {
                filterContext.Result = this.InvalidIdErrorView(
                    InstanceNames.DocumentsControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidPageNumberException)
            {
                filterContext.Result = this.InvalidPageNumberErrorView(
                    InstanceNames.DocumentsControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidItemsPerPageException)
            {
                filterContext.Result = this.InvalidNumberOfItemsPerPageErrorView(
                    InstanceNames.DocumentsControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is ArgumentException)
            {
                filterContext.Result = this.BadRequestErrorView(
                    InstanceNames.DocumentsControllerInstanceName,
                    filterContext.Exception.Message);
            }
            else
            {
                filterContext.Result = this.DefaultErrorView(
                    InstanceNames.DocumentsControllerInstanceName,
                    filterContext.Exception.Message);
            }

            filterContext.ExceptionHandled = true;
        }
    }
}
