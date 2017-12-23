namespace ProcessingTools.Data.Common.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Extensions;

    public class RepositoryProviderAsync<TRepository> : IGenericRepositoryProvider<TRepository>
        where TRepository : IRepository
    {
        private readonly IRepositoryFactory<TRepository> repositoryFactory;

        public RepositoryProviderAsync(IRepositoryFactory<TRepository> repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

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
