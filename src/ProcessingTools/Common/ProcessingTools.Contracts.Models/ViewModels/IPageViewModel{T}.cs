// <copyright file="IPageViewModel{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.ViewModels
{
    /// <summary>
    /// Generic page view model.
    /// </summary>
    /// <typeparam name="T">Type of page model.</typeparam>
    public interface IPageViewModel<out T> : IPageViewModel
    {
        /// <summary>
        /// Gets the page model.
        /// </summary>
        T Model { get; }
    }
}
