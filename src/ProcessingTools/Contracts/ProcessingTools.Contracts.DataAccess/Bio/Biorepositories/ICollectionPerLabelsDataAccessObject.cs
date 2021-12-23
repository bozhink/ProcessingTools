// <copyright file="ICollectionPerLabelsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Bio.Biorepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;

    /// <summary>
    /// Biorepository collection per labels data access object (DAO).
    /// </summary>
    public interface ICollectionPerLabelsDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Insert collection per label records.
        /// </summary>
        /// <param name="collectionPerLabels">List of collection per label records to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertCollectionPerLabelsAsync(IEnumerable<ICollectionPerLabel> collectionPerLabels);
    }
}
