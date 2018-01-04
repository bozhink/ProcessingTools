// <copyright file="RepositoryProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Data.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;

    /// <summary>
    /// Generic repository provider.
    /// </summary>
    /// <typeparam name="TRepository">Type of repository.</typeparam>
    public class RepositoryProvider<TRepository> : IGenericRepositoryProvider<TRepository>
        where TRepository : class, IRepository
    {
        private readonly TRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryProvider{TRepository}"/> class.
        /// </summary>
        /// <param name="repository">Repository.</param>
        public RepositoryProvider(TRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public Task ExecuteAsync(Func<TRepository, Task> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return action.Invoke(this.repository);
        }

        /// <inheritdoc/>
        public Task<T> ExecuteAsync<T>(Func<TRepository, Task<T>> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return action.Invoke(this.repository);
        }

        /// <inheritdoc/>
        public Task ExecuteAsync(Action<TRepository> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action.Invoke(this.repository);

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<T> ExecuteAsync<T>(Func<TRepository, T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var result = action.Invoke(this.repository);

            return Task.FromResult(result);
        }
    }
}
