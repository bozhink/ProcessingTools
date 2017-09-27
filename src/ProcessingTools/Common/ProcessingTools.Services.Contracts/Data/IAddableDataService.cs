// <copyright file="IAddableDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data
{
    using System.Threading.Tasks;

    public interface IAddableDataService<T>
    {
        Task<object> Add(params T[] items);
    }
}
