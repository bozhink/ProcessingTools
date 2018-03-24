// <copyright file="GridViewModel{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Shared
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Generic grid view model.
    /// </summary>
    /// <typeparam name="T">Type of the grid item.</typeparam>
    public class GridViewModel<T>
    {
        private readonly long numberOfItems;
        private readonly long numberOfItemsPerPage;
        private readonly long currentPage;
        private readonly IEnumerable<T> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridViewModel{T}"/> class.
        /// </summary>
        /// <param name="numberOfItems">The number of grid items.</param>
        /// <param name="numberOfItemsPerPage">The number of items per page.</param>
        /// <param name="currentPage">The value number of the current page.</param>
        /// <param name="items">The grid items.</param>
        public GridViewModel(long numberOfItems, long numberOfItemsPerPage, long currentPage, IEnumerable<T> items)
        {
            this.items = items ?? throw new ArgumentNullException(nameof(items));

            this.numberOfItems = numberOfItems;
            this.numberOfItemsPerPage = numberOfItemsPerPage;
            this.currentPage = currentPage;
        }

        /// <summary>
        /// Gets or sets the callback route values.
        /// </summary>
        public RouteViewModel CallbackRoute { get; set; }

        /// <summary>
        /// Gets items.
        /// </summary>
        public IEnumerable<T> Items => this.items;

        /// <summary>
        /// Gets the number of items per page.
        /// </summary>
        public long NumberOfItemsPerPage => this.numberOfItemsPerPage;

        /// <summary>
        /// Gets the current page.
        /// </summary>
        public long CurrentPage => this.currentPage;

        /// <summary>
        /// Gets the number of pages.
        /// </summary>
        public long NumberOfPages => (this.NumberOfItems % this.NumberOfItemsPerPage) == 0 ? this.NumberOfItems / this.NumberOfItemsPerPage : (this.NumberOfItems / this.NumberOfItemsPerPage) + 1;

        /// <summary>
        /// Gets the first name number.
        /// </summary>
        public long FirstPage => 0;

        /// <summary>
        /// Gets the last page number.
        /// </summary>
        public long LastPage => this.NumberOfPages - 1;

        /// <summary>
        /// Gets the previous page number.
        /// </summary>
        public long PreviousPage => this.CurrentPage == this.FirstPage ? this.CurrentPage : this.CurrentPage - 1;

        /// <summary>
        /// Gets the next page number.
        /// </summary>
        public long NextPage => this.CurrentPage == this.LastPage ? this.CurrentPage : this.CurrentPage + 1;

        /// <summary>
        /// Gets the number of items.
        /// </summary>
        public long NumberOfItems => this.numberOfItems;
    }
}
