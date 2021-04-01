// <copyright file="IInstitutionsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Bio.Biorepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;

    /// <summary>
    /// Biorepository institutions data access object (DAO).
    /// </summary>
    public interface IInstitutionsDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Insert institution records.
        /// </summary>
        /// <param name="institutions">List of institution records to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertInstitutionsAsync(IEnumerable<IInstitution> institutions);
    }
}
