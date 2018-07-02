// <copyright file="GeoEpithetsIndexPageViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.GeoEpithets
{
    using ProcessingTools.Models.Contracts.ViewModels;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// GeoEpithets index page view model
    /// </summary>
    public class GeoEpithetsIndexPageViewModel : IPageViewModel<ListWithPagingViewModel<GeoEpithetViewModel>>
    {
        /// <summary>
        /// Gets or sets model data.
        /// </summary>
        public ListWithPagingViewModel<GeoEpithetViewModel> Model { get; set; }

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
