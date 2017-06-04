namespace ProcessingTools.Data.Common.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Contracts.Data.Repositories;

    public class RepositoryProvider<TRepository> : IGenericRepositoryProvider<TRepository>
        where TRepository : IRepository
    {
        private readonly IRepositoryFactory<TRepository> repositoryFactory;

        public RepositoryProvider(IRepositoryFactory<TRepository> repositoryFactory)
        {
            if (repositoryFactory == null)
            {
                throw new ArgumentNullException(nameof(repositoryFactory));
            }

            this.repositoryFactory = repositoryFactory;
        }

        public Task Execute(Func<TRepository, Task> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Run(() =>
            {
                var repository = this.repositoryFactory.Create();
                try
                {
                    function.Invoke(repository).Wait();
                }
                finally
                {
                    repository.TryDispose();
                }
            });
        }

        public Task<T> Execute<T>(Func<TRepository, Task<T>> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return Task.Run(() =>
            {
                var repository = this.repositoryFactory.Create();
                try
                {
                    return function.Invoke(repository).Result;
                }
                finally
                {
                    repository.TryDispose();
                }
            });
        }

        public Task Execute(Action<TRepository> action)
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
    }
}
