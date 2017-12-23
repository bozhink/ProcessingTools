namespace ProcessingTools.Data.Common.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;

    public class RepositoryProvider<TRepository> : IGenericRepositoryProvider<TRepository>
        where TRepository : class, IRepository
    {
        private readonly TRepository repository;

        public RepositoryProvider(TRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task ExecuteAsync(Func<TRepository, Task> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return action.Invoke(this.repository);
        }

        public Task<T> ExecuteAsync<T>(Func<TRepository, Task<T>> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return action.Invoke(this.repository);
        }

        public Task ExecuteAsync(Action<TRepository> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action.Invoke(this.repository);

            return Task.CompletedTask;
        }

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
