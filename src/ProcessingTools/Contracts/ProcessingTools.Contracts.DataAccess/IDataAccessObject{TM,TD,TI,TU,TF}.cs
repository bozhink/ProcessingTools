// <copyright file="IDataAccessObject{TM,TD,TI,TU,TF}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
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
    /// <typeparam name="TF">Type of the filter model.</typeparam>
    public interface IDataAccessObject<TM, TD, TI, TU, TF> : IDataAccessObject<TM, TD, TI, TU>
        where TM : IDataTransferObject
        where TD : IDataTransferObject
    {
        /// <summary>
        /// Select items.
        /// </summary>
        /// <param name="filter">Filter to be applied.</param>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Selected items.</returns>
        Task<IList<TM>> SelectAsync(TF filter, int skip, int take);

        /// <summary>
        /// Select items with details.
        /// </summary>
        /// <param name="filter">Filter to be applied.</param>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Selected items.</returns>
        Task<IList<TD>> SelectDetailsAsync(TF filter, int skip, int take);

        /// <summary>
        /// Selects the count of all items in the data store.
        /// </summary>
        /// <param name="filter">Filter to be applied.</param>
        /// <returns>The long count of all items in the data store.</returns>
        Task<long> SelectCountAsync(TF filter);
    }
}
