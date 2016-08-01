﻿namespace ProcessingTools.Web.Common.ViewModels.Contracts
{
    public interface IPagingViewModel
    {
        string ActionName { get; }

        long CurrentPage { get; }

        long FirstPage { get; }

        long LastPage { get; }

        long NextPage { get; }

        long NumberOfItemsPerPage { get; }

        long NumberOfPages { get; }

        long PreviousPage { get; }
    }
}
