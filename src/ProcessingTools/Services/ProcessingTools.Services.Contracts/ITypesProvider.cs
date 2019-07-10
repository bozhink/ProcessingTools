// <copyright file="ITypesProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Provider of <see cref="Type"/> objects.
    /// </summary>
    public interface ITypesProvider
    {
        /// <summary>
        /// Gets types.
        /// </summary>
        /// <returns>Found types.</returns>
        IEnumerable<Type> GetTypes();

        /// <summary>
        /// Gets types.
        /// </summary>
        /// <returns>Found types.</returns>
        Task<IEnumerable<Type>> GetTypesAsync();
    }
}
