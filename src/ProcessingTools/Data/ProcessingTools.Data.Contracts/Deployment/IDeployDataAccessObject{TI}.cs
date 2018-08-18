// <copyright file="IDeployDataAccessObject{TI}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Deployment
{
    using System.Threading.Tasks;

    /// <summary>
    /// Deploy data access object.
    /// </summary>
    /// <typeparam name="TI">Type of the insert data model.</typeparam>
    public interface IDeployDataAccessObject<TI> : IDataAccessObject
    {
        /// <summary>
        /// Inserts item to the data store.
        /// </summary>
        /// <param name="model">Item to be inserted.</param>
        /// <returns>Task</returns>
        Task<object> InsertAsync(TI model);
    }
}
