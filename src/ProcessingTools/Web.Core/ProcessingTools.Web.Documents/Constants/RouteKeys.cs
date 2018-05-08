// <copyright file="RouteKeys.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Constants
{
    /// <summary>
    /// Route keys.
    /// </summary>
    public static class RouteKeys
    {
        /// <summary>
        /// Area.
        /// </summary>
        /// <example>
        /// ViewContext.RouteData.Values["area"]
        /// </example>
        public const string Area = "area";

        /// <summary>
        /// Controller.
        /// </summary>
        /// <example>
        /// ViewContext.RouteData.Values["controller"]
        /// </example>
        public const string Controller = "controller";

        /// <summary>
        /// Action.
        /// </summary>
        /// <example>
        /// ViewContext.RouteData.Values["action"]
        /// </example>
        public const string Action = "action";
    }
}
