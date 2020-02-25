// <copyright file="ICollectionLabelsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Bio.Biorepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;

    /// <summary>
    /// Biorepository collection labels data access object (DAO).
    /// </summary>
    public interface ICollectionLabelsDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Insert collection label records.
        /// </summary>
        /// <param name="collectionLabels">List of collection labels to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertCollectionLabelsAsync(IEnumerable<ICollectionLabel> collectionLabels);
    }
}
