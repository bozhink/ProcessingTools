// <copyright file="IDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess
{
    using System.Threading.Tasks;

    /// <summary>
    /// Data access object (DAO).
    /// </summary>
    public interface IDataAccessObject
    {
        /// <summary>
        /// Save changes.
        /// </summary>
        /// <returns>Number of saved changes. -1 if not applicable.</returns>
        Task<long> SaveChangesAsync();
    }
}
