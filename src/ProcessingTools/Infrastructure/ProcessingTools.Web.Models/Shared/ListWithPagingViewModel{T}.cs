// <copyright file="ListWithPagingViewModel{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Shared
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.ViewModels;

    /// <summary>
    /// Represents implementation of the <see cref="IPagingViewModel"/>.
    /// </summary>
    /// <typeparam name="T">Type of the item</typeparam>
    public class ListWithPagingViewModel<T> : IPagingViewModel
    {
        private readonly long numberOfItems;
        private readonly long numberOfItemsPerPage;
        private readonly long currentPage;
        private readonly IEnumerable<T> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListWithPagingViewModel{T}"/> class.
        /// </summary>
        /// <param name="actionName">Name of action to build navigation links</param>
        /// <param name="numberOfItems">Total number of items</param>
        /// <param name="numberOfItemsPerPage">Number of items per page</param>
        /// <param name="currentPage">The number of the current page</param>
        /// <param name="items">The items to be paginated</param>
        public ListWithPagingViewModel(string actionName, long numberOfItems, long numberOfItemsPerPage, long currentPage, IEnumerable<T> items)
        {
            if (string.IsNullOrWhiteSpace(actionName))
            {
                throw new ArgumentNullException(nameof(actionName));
            }

            this.ActionName = actionName;
            this.items = items ?? throw new ArgumentNullException(nameof(items));

            this.numberOfItems = numberOfItems;
            this.numberOfItemsPerPage = numberOfItemsPerPage;
            this.currentPage = currentPage;
        }

        /// <inheritdoc/>
        public string ActionName { get; private set; }

        /// <summary>
        /// Gets items to be paginated.
        /// </summary>
        public IEnumerable<T> Items => this.items;

        /// <inheritdoc/>
        public long NumberOfItemsPerPage => this.numberOfItemsPerPage;

        /// <inheritdoc/>
        public long CurrentPage => this.currentPage;

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

        private long NumberOfItems => this.numberOfItems;
    }
}
