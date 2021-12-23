// <copyright file="ICollectionPersDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Bio.Biorepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;

    /// <summary>
    /// Biorepository collection pers data access object (DAO).
    /// </summary>
    public interface ICollectionPersDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Insert collection per records.
        /// </summary>
        /// <param name="collectionPers">List of collection per records to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertCollectionPersAsync(IEnumerable<ICollectionPer> collectionPers);
    }
}
