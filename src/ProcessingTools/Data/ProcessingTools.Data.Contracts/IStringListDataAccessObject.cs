// <copyright file="IStringListDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// String list data access object.
    /// </summary>
    public interface IStringListDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Inserts one string item to the data list.
        /// </summary>
        /// <param name="item">Item to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertOneAsync(string item);

        /// <summary>
        /// Inserts many string items to the data list.
        /// </summary>
        /// <param name="items">Items to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertManyAsync(IEnumerable<string> items);

        /// <summary>
        /// Deletes one string item from the data list.
        /// </summary>
        /// <param name="item">Item to be deleted.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteOneAsync(string item);

        /// <summary>
        /// Deletes many string items from the data list.
        /// </summary>
        /// <param name="items">Items to be deleted.</param>
        /// <returns>Task of result.</returns>
        Task<object> DeleteManyAsync(IEnumerable<string> items);

        /// <summary>
        /// Finds string items by matching filter.
        /// </summary>
        /// <param name="filter">Filter value to be applied.</param>
        /// <returns>Task of result as string array.</returns>
        Task<string[]> FindAsync(string filter);

        /// <summary>
        /// Gets all string items from the data list.
        /// </summary>
        /// <returns>Task of result as string array.</returns>
        Task<string[]> GetAllAsync();
    }
}
