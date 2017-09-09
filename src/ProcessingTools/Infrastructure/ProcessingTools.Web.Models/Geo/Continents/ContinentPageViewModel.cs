// <copyright file="ContinentPageViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Geo.Continents
{
    using ProcessingTools.Contracts.ViewModels;

    /// <summary>
    /// Continent page view model
    /// </summary>
    public class ContinentPageViewModel : IPageViewModel<ContinentViewModel>
    {
        /// <summary>
        /// Gets or sets model data.
        /// </summary>
        public ContinentViewModel Model { get; set; }

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
