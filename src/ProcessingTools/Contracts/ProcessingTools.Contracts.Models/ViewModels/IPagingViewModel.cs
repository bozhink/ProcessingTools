﻿// <copyright file="IPagingViewModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.ViewModels
{
    /// <summary>
    /// View model with paging
    /// </summary>
    public interface IPagingViewModel : IPagedViewModel
    {
        /// <summary>
        /// Gets the name of action to build navigation links.
        /// </summary>
        string ActionName { get; }
    }
}