﻿// <copyright file="IPageViewModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.ViewModels
{
    /// <summary>
    /// Page view model.
    /// </summary>
    public interface IPageViewModel
    {
        /// <summary>
        /// Gets page title.
        /// </summary>
        string PageTitle { get; }

        /// <summary>
        /// Gets page heading.
        /// </summary>
        string PageHeading { get; }

        /// <summary>
        /// Gets return-URL.
        /// </summary>
        string ReturnUrl { get; }
    }
}
