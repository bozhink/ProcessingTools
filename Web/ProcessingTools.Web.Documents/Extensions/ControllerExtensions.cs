namespace ProcessingTools.Web.Documents.Extensions
{
    using System;
    using System.Net;
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
    }
}
