// <copyright file="IDeployDataAccessObject{TI}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Deployment
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
        /// <returns>Resultant object.</returns>
        Task<object> InsertAsync(TI model);
    }
}
