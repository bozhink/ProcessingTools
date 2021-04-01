// <copyright file="IStringItemsProvider.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Provider of string items.
    /// </summary>
    public interface IStringItemsProvider
    {
        /// <summary>
        /// Get items.
        /// </summary>
        /// <returns>Task of items.</returns>
        Task<IList<string>> GetItemsAsync();
    }
}
