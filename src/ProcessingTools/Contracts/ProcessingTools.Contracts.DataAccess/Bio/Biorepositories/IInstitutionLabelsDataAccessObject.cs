// <copyright file="IInstitutionLabelsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Bio.Biorepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;

    /// <summary>
    /// Biorepository institution labels data access object (DAO).
    /// </summary>
    public interface IInstitutionLabelsDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Insert institution label records.
        /// </summary>
        /// <param name="institutionLabels">List of institution label records to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertInstitutionLabelsAsync(IEnumerable<IInstitutionLabel> institutionLabels);
    }
}
