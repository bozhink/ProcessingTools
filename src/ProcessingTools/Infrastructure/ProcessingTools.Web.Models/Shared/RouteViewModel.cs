// <copyright file="RouteViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Shared
{
    /// <summary>
    /// Route view model
    /// </summary>
    public class RouteViewModel
    {
        /// <summary>
        /// Gets or sets action name.
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets controller name.
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets route values.
        /// </summary>
        public object RouteValues { get; set; }
    }
}
