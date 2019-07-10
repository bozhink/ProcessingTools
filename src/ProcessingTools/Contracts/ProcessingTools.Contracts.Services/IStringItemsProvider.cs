// <copyright file="IStringItemsProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services
{
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
