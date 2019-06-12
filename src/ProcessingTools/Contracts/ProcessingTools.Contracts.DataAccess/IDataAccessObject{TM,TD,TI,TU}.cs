// <copyright file="IDataAccessObject{TM,TD,TI,TU}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Models;

    /// <summary>
    /// Generic data access object (DAO).
    /// </summary>
    /// <typeparam name="TM">Type of the data transfer object.</typeparam>
    /// <typeparam name="TD">Type of the detailed data transfer object.</typeparam>
    /// <typeparam name="TI">Type of the insert data model.</typeparam>
    /// <typeparam name="TU">Type of the update data model.</typeparam>
    public interface IDataAccessObject<TM, TD, TI, TU> : IDataAccessObject
        where TM : IDataModel
        where TD : IDataModel
    {
        /// <summary>
        /// Inserts item to the data store.
        /// </summary>
        /// <param name="model">Item to be inserted.</param>
        /// <returns>Inserted model.</returns>
        Task<TM> InsertAsync(TI model);

        /// <summary>
        /// Updates item in the data store.
        /// </summary>
        /// <param name="model">Item with updated fields.</param>
        /// <returns>Updated model.</returns>
        Task<TM> UpdateAsync(TU model);

        /// <summary>
        /// Delete item from the data store.
        /// </summary>
        /// <param name="id">ID of the item to be deleted.</param>
        /// <returns>Result of operation.</returns>
        Task<object> DeleteAsync(object id);

        /// <summary>
        /// Gets item by its ID.
        /// </summary>
        /// <param name="id">ID of the item.</param>
        /// <returns>Corresponding item of the provided ID.</returns>
        Task<TM> GetByIdAsync(object id);

        /// <summary>
        /// Gets detailed item by its ID.
        /// </summary>
        /// <param name="id">ID of the item.</param>
        /// <returns>Corresponding item of the provided ID.</returns>
        Task<TD> GetDetailsByIdAsync(object id);

        /// <summary>
        /// Select items.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Selected items.</returns>
        Task<IList<TM>> SelectAsync(int skip, int take);

        /// <summary>
        /// Select items with details.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Selected items.</returns>
        Task<IList<TD>> SelectDetailsAsync(int skip, int take);

        /// <summary>
        /// Selects the count of all items in the data store.
        /// </summary>
        /// <returns>The long count of all items in the data store.</returns>
        Task<long> SelectCountAsync();
    }
}
