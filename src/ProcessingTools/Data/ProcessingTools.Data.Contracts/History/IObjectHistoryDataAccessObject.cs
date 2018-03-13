// <copyright file="IObjectHistoryDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.History
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.History;

    /// <summary>
    /// Object history data access object.
    /// </summary>
    public interface IObjectHistoryDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Adds new object history item to data store.
        /// </summary>
        /// <param name="objectHistory">Object history item to be added.</param>
        /// <returns>Inserted object history item.</returns>
        Task<IObjectHistory> AddAsync(IObjectHistory objectHistory);

        /// <summary>
        /// Gets all object histories for object with specified ObjectId.
        /// </summary>
        /// <param name="objectId">ObjectId of the source object.</param>
        /// <returns>Array of object histories.</returns>
        Task<IObjectHistory[]> GetAsync(object objectId);

        /// <summary>
        /// Gets object histories for object with specified ObjectId.
        /// </summary>
        /// <param name="objectId">ObjectId of the source object.</param>
        /// <param name="skip">Number of object histories to skip.</param>
        /// <param name="take">Number of object histories to take.</param>
        /// <returns>Array of object histories.</returns>
        Task<IObjectHistory[]> GetAsync(object objectId, int skip, int take);
    }
}
