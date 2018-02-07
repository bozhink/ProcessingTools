// <copyright file="IRepositoryFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    /// <summary>
    /// Repository factory.
    /// </summary>
    /// <typeparam name="TRepository">Type of the repository.</typeparam>
    public interface IRepositoryFactory<out TRepository>
        where TRepository : IRepository
    {
        /// <summary>
        /// Creates repository instance.
        /// </summary>
        /// <returns>The repository instance.</returns>
        TRepository Create();
    }
}
