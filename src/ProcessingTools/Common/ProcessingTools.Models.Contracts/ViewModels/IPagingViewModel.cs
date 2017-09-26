// <copyright file="IPagingViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.ViewModels
{
    /// <summary>
    /// View model with paging
    /// </summary>
    public interface IPagingViewModel
    {
        /// <summary>
        /// Gets the name of action to build navigation links.
        /// </summary>
        string ActionName { get; }

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        long CurrentPage { get; }

        /// <summary>
        /// Gets the first page number.
        /// </summary>
        long FirstPage { get; }

        /// <summary>
        /// Gets the last page number.
        /// </summary>
        long LastPage { get; }

        /// <summary>
        /// Gets the next page number.
        /// </summary>
        long NextPage { get; }

        /// <summary>
        /// Gets the next page number.
        /// </summary>
        long PreviousPage { get; }

        /// <summary>
        /// Gets the number of items per page.
        /// </summary>
        long NumberOfItemsPerPage { get; }

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        long NumberOfPages { get; }
    }
}
