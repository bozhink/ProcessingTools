// <copyright file="IGenericRepositoryProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories
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
        /// Execute action on repository.
        /// </summary>
        /// <param name="action">Action to be executed.</param>
        /// <returns>Task.</returns>
        Task ExecuteAsync(Action<TRepository> action);

        /// <summary>
        /// Execute awaitable function on repository.
        /// </summary>
        /// <param name="action">Awaitable function to be executed.</param>
        /// <returns>Task.</returns>
        Task ExecuteAsync(Func<TRepository, Task> action);

        /// <summary>
        /// Executes generic function on repository.
        /// </summary>
        /// <typeparam name="T">Type of returned result.</typeparam>
        /// <param name="action">Function to be executed.</param>
        /// <returns>Task of result.</returns>
        Task<T> ExecuteAsync<T>(Func<TRepository, T> action);

        /// <summary>
        /// Executes generic function on repository.
        /// </summary>
        /// <typeparam name="T">Type of returned result.</typeparam>
        /// <param name="action">Function to be executed.</param>
        /// <returns>Task of result.</returns>
        Task<T> ExecuteAsync<T>(Func<TRepository, Task<T>> action);
    }
}
