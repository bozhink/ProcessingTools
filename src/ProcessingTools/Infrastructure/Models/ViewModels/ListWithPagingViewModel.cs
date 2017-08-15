namespace ProcessingTools.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts.ViewModels;

    public class ListWithPagingViewModel<T> : IPagingViewModel
    {
        private readonly long numberOfItems;
        private readonly long numberOfItemsPerPage;
        private readonly long currentPage;
        private readonly IEnumerable<T> items;

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

        public string ActionName { get; private set; }

        public IEnumerable<T> Items => this.items;

        public long NumberOfItemsPerPage => this.numberOfItemsPerPage;

        public long CurrentPage => this.currentPage;

        public long NumberOfPages => (this.NumberOfItems % this.NumberOfItemsPerPage) == 0 ? this.NumberOfItems / this.NumberOfItemsPerPage : (this.NumberOfItems / this.NumberOfItemsPerPage) + 1;

        public long FirstPage => 0;

        public long LastPage => this.NumberOfPages - 1;

        public long PreviousPage => this.CurrentPage == this.FirstPage ? this.CurrentPage : this.CurrentPage - 1;

        public long NextPage => this.CurrentPage == this.LastPage ? this.CurrentPage : this.CurrentPage + 1;

        private long NumberOfItems => this.numberOfItems;
    }
}
