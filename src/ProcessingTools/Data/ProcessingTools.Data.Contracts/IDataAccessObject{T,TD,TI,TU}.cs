// <copyright file="IDataAccessObject{T,TD,TI,TU}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Data.Models.Contracts;

    /// <summary>
    /// Generic data access object (DAO).
    /// </summary>
    /// <typeparam name="T">Type of the data model.</typeparam>
    /// <typeparam name="TD">Type of the detailed data model.</typeparam>
    /// <typeparam name="TI">Type of the insert data model.</typeparam>
    /// <typeparam name="TU">Type of the update data model.</typeparam>
    public interface IDataAccessObject<T, TD, TI, TU> : IDataAccessObject
        where T : IDataModel
        where TD : IDataModel
    {
        /// <summary>
        /// Inserts item to the data store.
        /// </summary>
        /// <param name="model">Item to be inserted.</param>
        /// <returns>Task</returns>
        Task<T> InsertAsync(TI model);

        /// <summary>
        /// Updates item in the data store.
        /// </summary>
        /// <param name="model">Item with updated fields.</param>
        /// <returns>Task</returns>
        Task<T> UpdateAsync(TU model);

        /// <summary>
        /// Delete item from the data store.
        /// </summary>
        /// <param name="id">ID of the item to be deleted.</param>
        /// <returns>Task</returns>
        Task<object> DeleteAsync(object id);

        /// <summary>
        /// Select items.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Task of items.</returns>
        Task<T[]> SelectAsync(int skip, int take);

        /// <summary>
        /// Select items with details.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Task of items.</returns>
        Task<TD[]> SelectDetailsAsync(int skip, int take);

        /// <summary>
        /// Selects the count of all items in the data store.
        /// </summary>
        /// <returns>The long count of all items in the data store.</returns>
        Task<long> SelectCountAsync();

        /// <summary>
        /// Gets item by its ID.
        /// </summary>
        /// <param name="id">ID of the item.</param>
        /// <returns>Corresponding item of the provided ID.</returns>
        Task<T> GetById(object id);

        /// <summary>
        /// Gets detailed item by its ID.
        /// </summary>
        /// <param name="id">ID of the item.</param>
        /// <returns>Corresponding item of the provided ID.</returns>
        Task<TD> GetDetailsById(object id);
    }
}
