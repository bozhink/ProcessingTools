namespace ProcessingTools.Contracts.Data.Repositories
{
    using System;
    using System.Threading.Tasks;

    public interface IGenericRepositoryProvider<TRepository>
        where TRepository : IRepository
    {
        Task Execute(Action<TRepository> action);

        Task Execute(Func<TRepository, Task> function);
    }
}
