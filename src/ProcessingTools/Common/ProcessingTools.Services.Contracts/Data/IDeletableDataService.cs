// <copyright file="IDeletableDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data
{
    using System.Threading.Tasks;

    public interface IDeletableDataService<T>
    {
        Task<object> Delete(params T[] items);
    }
}
