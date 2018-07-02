// <copyright file="ContinentsIndexPageViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.Continents
{
    using ProcessingTools.Models.Contracts.ViewModels;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Continents index page view model
    /// </summary>
    public class ContinentsIndexPageViewModel : IPageViewModel<ListWithPagingViewModel<ContinentViewModel>>
    {
        /// <summary>
        /// Gets or sets model data.
        /// </summary>
        public ListWithPagingViewModel<ContinentViewModel> Model { get; set; }

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
