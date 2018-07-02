// <copyright file="IRepositoryProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    /// <summary>
    /// Generic repository provider.
    /// </summary>
    /// <typeparam name="TRepository">Type of repository.</typeparam>
    public interface IRepositoryProvider<out TRepository>
        where TRepository : IRepository
    {
        /// <summary>
        /// Creates new repository instance.
        /// </summary>
        /// <returns>New repository instance.</returns>
        TRepository Create();
    }
}
