// <copyright file="IStringItemsProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
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
        /// <returns>String items.</returns>
        IEnumerable<string> GetItems();

        /// <summary>
        /// Get items.
        /// </summary>
        /// <returns>Task</returns>
        Task<IEnumerable<string>> GetItemsAsync();
    }
}
