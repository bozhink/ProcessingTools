namespace ProcessingTools.Web.Documents.Abstractions
{
    using System;
    using System.Web.Mvc;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Web.Documents.Extensions;

    public abstract class MvcControllerWithExceptionHandling : Controller
    {
        protected abstract string InstanceName { get; }

        protected override void HandleUnknownAction(string actionName)
        {
            this.IvalidActionErrorView(actionName).ExecuteResult(this.ControllerContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is EntityNotFoundException)
            {
                filterContext.Result = this.DefaultNotFoundView(
                    this.InstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidIdException)
            {
                filterContext.Result = this.InvalidIdErrorView(
                    this.InstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidPageNumberException)
            {
                filterContext.Result = this.InvalidPageNumberErrorView(
                    this.InstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is InvalidItemsPerPageException)
            {
                filterContext.Result = this.InvalidNumberOfItemsPerPageErrorView(
                    this.InstanceName,
                    filterContext.Exception.Message);
            }
            else if (filterContext.Exception is ArgumentException)
            {
                filterContext.Result = this.BadRequestErrorView(
                    this.InstanceName,
                    filterContext.Exception.Message);
            }
            else
            {
                filterContext.Result = this.DefaultErrorView(
                    this.InstanceName,
                    filterContext.Exception.Message);
            }

            filterContext.ExceptionHandled = true;
        }
    }
}
