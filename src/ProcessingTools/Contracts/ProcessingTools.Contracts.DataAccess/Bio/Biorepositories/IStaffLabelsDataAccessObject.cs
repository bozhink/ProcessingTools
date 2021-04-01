// <copyright file="IStaffLabelsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Bio.Biorepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;

    /// <summary>
    /// Biorepository staff labels data access object (DAO).
    /// </summary>
    public interface IStaffLabelsDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Insert staff label records.
        /// </summary>
        /// <param name="staffLabels">Staff label records to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertStaffLabelsAsync(IEnumerable<IStaffLabel> staffLabels);
    }
}
