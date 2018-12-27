// <copyright file="IGenericRepositoryProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic repository provider.
    /// </summary>
    /// <typeparam name="TRepository">Type of repository.</typeparam>
    public interface IGenericRepositoryProvider<TRepository>
        where TRepository : IRepository
    {
        /// <summary>
        /// Execute awaitable function on repository.
        /// </summary>
        /// <param name="action">Awaitable function to be executed.</param>
        /// <returns>Task.</returns>
        Task ExecuteAsync(Func<TRepository, Task> action);
    }
}
