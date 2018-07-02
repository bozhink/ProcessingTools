// <copyright file="GridViewModel{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Shared
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.ViewModels;

    /// <summary>
    /// Generic grid view model.
    /// </summary>
    /// <typeparam name="T">Type of the grid item.</typeparam>
    public class GridViewModel<T> : IPagedViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridViewModel{T}"/> class.
        /// </summary>
        /// <param name="numberOfItems">The number of grid items.</param>
        /// <param name="numberOfItemsPerPage">The number of items per page.</param>
        /// <param name="currentPage">The value number of the current page.</param>
        /// <param name="items">The grid items.</param>
        public GridViewModel(long numberOfItems, long numberOfItemsPerPage, long currentPage, IEnumerable<T> items)
        {
            this.Items = items ?? throw new ArgumentNullException(nameof(items));

            this.NumberOfItems = numberOfItems;
            this.NumberOfItemsPerPage = numberOfItemsPerPage;
            this.CurrentPage = currentPage;
        }

        /// <summary>
        /// Gets or sets the callback route values.
        /// </summary>
        public RouteViewModel CallbackRoute { get; set; }

        /// <summary>
        /// Gets items.
        /// </summary>
        public IEnumerable<T> Items { get; }

        /// <summary>
        /// Gets the number of items.
        /// </summary>
        public long NumberOfItems { get; }

        /// <inheritdoc/>
        public long NumberOfItemsPerPage { get; }

        /// <inheritdoc/>
        public long CurrentPage { get; }

        /// <inheritdoc/>
        public long NumberOfPages => (this.NumberOfItems % this.NumberOfItemsPerPage) == 0 ? this.NumberOfItems / this.NumberOfItemsPerPage : (this.NumberOfItems / this.NumberOfItemsPerPage) + 1;

        /// <inheritdoc/>
        public long FirstPage => 0;

        /// <inheritdoc/>
        public long LastPage => this.NumberOfPages - 1;

        /// <inheritdoc/>
        public long PreviousPage => this.CurrentPage == this.FirstPage ? this.CurrentPage : this.CurrentPage - 1;

        /// <inheritdoc/>
        public long NextPage => this.CurrentPage == this.LastPage ? this.CurrentPage : this.CurrentPage + 1;
    }
}
