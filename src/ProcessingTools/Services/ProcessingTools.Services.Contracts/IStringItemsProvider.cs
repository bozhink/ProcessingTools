// <copyright file="IStringItemsProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts
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
        /// <returns>Task</returns>
        Task<IList<string>> GetItemsAsync();
    }
}
