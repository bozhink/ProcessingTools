// <copyright file="IStaffDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Bio.Biorepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;

    /// <summary>
    /// Biorepository staff data access object (DAO).
    /// </summary>
    public interface IStaffDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Insert staff records.
        /// </summary>
        /// <param name="staffs">List of staff records to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertStaffAsync(IEnumerable<IStaff> staffs);
    }
}
