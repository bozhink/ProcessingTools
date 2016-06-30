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
                    ActionLinkText = ContentConstants.DefaultBackToListActionLinkTitle,
                    ActionName = ControllerConstants.DeafultIndexActionName,
                    ControllerName = sourceAction.ControllerName,
                    AreaName = sourceAction.AreaName
                });
        }

        public static ViewResult ErrorViewWithGoBackToIndexDestination(
            this Controller controller,
            string viewName,
            HttpStatusCode responseStatusCode,
            string instanceName,
            string message,
            string actionLinkText,
            string areaName,
            int stackFrameLevel = 3)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrame = stackTrace.GetFrames();

            string controllerName = Regex.Replace(controller.GetType().Name, @"\A.*?\.|Controller\Z", string.Empty);
            string actionName = stackFrame[stackFrameLevel].GetMethod().Name;

            return controller.ErrorView(
                viewName,
                responseStatusCode,
                instanceName,
                new ActionMetaViewModel
                {
                    ActionLinkText = actionLinkText,
                    Message = message,
                    AreaName = areaName,
                    ControllerName = controllerName,
                    ActionName = actionName
                },
                new ActionMetaViewModel
                {
                    ActionLinkText = ContentConstants.DefaultBackToListActionLinkTitle,
                    ActionName = ControllerConstants.DeafultIndexActionName,
                    ControllerName = controllerName,
                    AreaName = areaName
                });
        }

        public static ViewResult NoFilesSelectedErrorView(this Controller controller, string instanceName, string message, string actionLinkText, string areaName, int stackFrameLevel = 4)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewConstants.NoFilesSelectedErrorViewName,
                HttpStatusCode.BadRequest,
                instanceName,
                message,
                actionLinkText,
                areaName,
                stackFrameLevel);
        }

        public static ViewResult InvalidOrEmptyFileErrorView(this Controller controller, string instanceName, string message, string actionLinkText, string areaName, int stackFrameLevel = 4)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewConstants.InvalidOrEmptyFileErrorViewName,
                HttpStatusCode.BadRequest,
                instanceName,
                message,
                actionLinkText,
                areaName,
                stackFrameLevel);
        }

        public static ViewResult DefaultErrorView(this Controller controller, string instanceName, string message, string actionLinkText, string areaName, int stackFrameLevel = 4)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewConstants.DefaultErrorViewName,
                HttpStatusCode.InternalServerError,
                instanceName,
                message,
                actionLinkText,
                areaName,
                stackFrameLevel);
        }

        public static ViewResult NullIdErrorViewName(this Controller controller, string instanceName, string message, string actionLinkText, string areaName, int stackFrameLevel = 4)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewConstants.NullIdErrorViewName,
                HttpStatusCode.BadRequest,
                instanceName,
                message,
                actionLinkText,
                areaName,
                stackFrameLevel);
        }

        public static ViewResult DefaultNotFoundViewName(this Controller controller, string instanceName, string message, string actionLinkText, string areaName, int stackFrameLevel = 4)
        {
            return controller.ErrorViewWithGoBackToIndexDestination(
                ViewConstants.DefaultNotFoundViewName,
                HttpStatusCode.NotFound,
                instanceName,
                message,
                actionLinkText,
                areaName,
                stackFrameLevel);
        }
    }
}
