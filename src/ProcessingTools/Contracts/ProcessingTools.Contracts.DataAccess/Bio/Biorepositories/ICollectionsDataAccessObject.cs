// <copyright file="ICollectionsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Bio.Biorepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;

    /// <summary>
    /// Biorepository collections data access object (DAO).
    /// </summary>
    public interface ICollectionsDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Insert collection records.
        /// </summary>
        /// <param name="collections">List of collection records to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertCollectionsAsync(IEnumerable<ICollection> collections);
    }
}
