// <copyright file="GeoNamePageViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.GeoNames
{
    using ProcessingTools.Models.Contracts.ViewModels;

    /// <summary>
    /// GeoName page view model
    /// </summary>
    public class GeoNamePageViewModel : IPageViewModel<GeoNameViewModel>
    {
        /// <summary>
        /// Gets or sets model data.
        /// </summary>
        public GeoNameViewModel Model { get; set; }

        /// <summary>
        /// Gets the page heading.
        /// </summary>
        public string PageHeading => this.PageTitle;

        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// Gets or sets the return-url.
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
