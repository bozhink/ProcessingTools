// <copyright file="RepositoryProviderAsync.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Data.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Generic repository provider.
    /// </summary>
    /// <typeparam name="TRepository">Type of repository.</typeparam>
    public class RepositoryProviderAsync<TRepository> : IGenericRepositoryProvider<TRepository>
        where TRepository : IRepository
    {
        private readonly IRepositoryFactory<TRepository> repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryProviderAsync{TRepository}"/> class.
        /// </summary>
        /// <param name="repositoryFactory">Repository factory.</param>
        public RepositoryProviderAsync(IRepositoryFactory<TRepository> repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <inheritdoc/>
        public Task ExecuteAsync(Func<TRepository, Task> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Task.Run(() =>
            {
                var repository = this.repositoryFactory.Create();
                try
                {
                    action.Invoke(repository).Wait();
                }
                finally
                {
                    repository.TryDispose();
                }
            });
        }

        /// <inheritdoc/>
        public Task<T> ExecuteAsync<T>(Func<TRepository, Task<T>> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Task.Run(() =>
            {
                var repository = this.repositoryFactory.Create();
                try
                {
                    return action.Invoke(repository).Result;
                }
                finally
                {
                    repository.TryDispose();
                }
            });
        }

        /// <inheritdoc/>
        public Task ExecuteAsync(Action<TRepository> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Task.Run(() =>
            {
                var repository = this.repositoryFactory.Create();
                try
                {
                    action.Invoke(repository);
                }
                finally
                {
                    repository.TryDispose();
                }
            });
        }

        /// <inheritdoc/>
        public Task<T> ExecuteAsync<T>(Func<TRepository, T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Task.Run(() =>
            {
                var repository = this.repositoryFactory.Create();
                try
                {
                    return action.Invoke(repository);
                }
                finally
                {
                    repository.TryDispose();
                }
            });
        }
    }
}
