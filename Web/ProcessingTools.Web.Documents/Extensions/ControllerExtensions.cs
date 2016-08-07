namespace ProcessingTools.Web.Documents.Extensions
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Documents.ViewModels.Error;

    public static class ControllerExtensions
    {
        private const string DefaultText = "";

        private const string AreaNameContextKey = "area";
        private const string ActionNameContextKey = "action";
        private const string ControllerNameContextKey = "controller";

        public static string GetCallingActionName(this Controller controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return controller.HttpContext.Request.RequestContext.RouteData.Values[ActionNameContextKey].ToString();
        }

        public static string GetCallingControllerName(this Controller controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return controller.HttpContext.Request.RequestContext.RouteData.Values[ControllerNameContextKey].ToString();
        }

        public static string GetCallingAreaName(this Controller controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return controller.HttpContext.Request.RequestContext.RouteData.DataTokens[AreaNameContextKey].ToString();
        }

        public static ViewResult ErrorView(
            this Controller controller,
            string viewName,
            HttpStatusCode responseStatusCode,
            string instanceName,
            ActionMetaViewModel sourceAction,
            params ActionMetaViewModel[] destinationActions)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (string.IsNullOrWhiteSpace(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            if (sourceAction == null)
            {
                throw new ArgumentNullException(nameof(sourceAction));
            }

            controller.Response.StatusCode = (int)responseStatusCode;

            var view = new ViewResult
            {
                ViewName = viewName,
                ViewData = new ViewDataDictionary(controller.ViewData)
                {
                    Model = new ErrorMetaViewModel
                    {
                        ErrorCode = (int)responseStatusCode,
                        Message = responseStatusCode.ToString(),
                        InstanceName = instanceName,
                        SourceAction = sourceAction,
                        DestinationActions = destinationActions
                    }
                }
            };

            return view;
        }

        public static ViewResult ErrorViewWithGoBackToIndexDestination(
            this Controller controller,
            string viewName,
            HttpStatusCode responseStatusCode,
            string instanceName,
            ActionMetaViewModel sourceAction)
        {
            return controller.ErrorView(
                viewName,
                responseStatusCode,
                instanceName,
                sourceAction,
                new ActionMetaViewModel
                {
                    ActionLinkText = Resources.Strings.DefaultBackToListActionLinkTitle,
                    ActionName = ActionNames.DeafultIndexActionName,
                    ControllerName = sourceAction.ControllerName,
                    AreaName = sourceAction.AreaName
                });
        }

        public static ViewResult ErrorViewWithGoBackToIndexDestination(this Controller controller, string viewName, HttpStatusCode responseStatusCode, string instanceName, string message = DefaultText, string actionLinkText = DefaultText)
        {
            string controllerName = controller.GetCallingControllerName();
            string actionName = controller.GetCallingActionName();
            string areaName = controller.GetCallingAreaName();

            string linkText = actionLinkText;
            if (string.IsNullOrEmpty(linkText))
            {
                linkText = Regex.Replace(actionName, @"(?<!\A)(?=[A-Z0-9]+)", " ");
            }

            return controller.ErrorView(
                viewName,
                responseStatusCode,
                instanceName,
                new ActionMetaViewModel
                {
                    ActionLinkText = linkText,
                    Message = message,
                    AreaName = areaName,
                    ControllerName = controllerName,
                    ActionName = actionName
                },
                new ActionMetaViewModel
                {
                    ActionLinkText = Resources.Strings.DefaultBackToListActionLinkTitle,
                    ActionName = ActionNames.DeafultIndexActionName,
                    ControllerName = controllerName,
                    AreaName = areaName
                });
        }

        public static ViewResult NoFilesSelectedErrorView(this Controller controller, string instanceName, string message = DefaultText, string actionLinkText = DefaultText)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewNames.NoFilesSelectedErrorViewName,
                HttpStatusCode.BadRequest,
                instanceName,
                message,
                actionLinkText);
        }

        public static ViewResult InvalidOrEmptyFilesErrorView(this Controller controller, string instanceName, string message = DefaultText, string actionLinkText = DefaultText)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewNames.InvalidOrEmptyFilesErrorViewName,
                HttpStatusCode.BadRequest,
                instanceName,
                message,
                actionLinkText);
        }

        public static ViewResult DefaultErrorView(this Controller controller, string instanceName, string message = DefaultText, string actionLinkText = DefaultText)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewNames.ErrorViewName,
                HttpStatusCode.InternalServerError,
                instanceName,
                message,
                actionLinkText);
        }

        public static ViewResult InvalidIdErrorView(this Controller controller, string instanceName, string message = DefaultText, string actionLinkText = DefaultText)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewNames.InvalidIdErrorViewName,
                HttpStatusCode.BadRequest,
                instanceName,
                message,
                actionLinkText);
        }

        public static ViewResult DefaultNotFoundView(this Controller controller, string instanceName, string message = DefaultText, string actionLinkText = DefaultText)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewNames.NotFoundErrorViewName,
                HttpStatusCode.NotFound,
                instanceName,
                message,
                actionLinkText);
        }

        public static ViewResult InvalidPageNumberErrorView(this Controller controller, string instanceName, string message = DefaultText, string actionLinkText = DefaultText)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewNames.InvalidPageNumberErrorViewName,
                HttpStatusCode.BadRequest,
                instanceName,
                message,
                actionLinkText);
        }

        public static ViewResult InvalidNumberOfItemsPerPageErrorView(this Controller controller, string instanceName, string message = DefaultText, string actionLinkText = DefaultText)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewNames.InvalidNumberOfItemsPerPageErrorViewName,
                HttpStatusCode.BadRequest,
                instanceName,
                message,
                actionLinkText);
        }

        public static ViewResult BadRequestErrorView(this Controller controller, string instanceName, string message = DefaultText, string actionLinkText = DefaultText)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewNames.BadRequestErrorViewName,
                HttpStatusCode.BadRequest,
                instanceName,
                message,
                actionLinkText);
        }
    }
}
