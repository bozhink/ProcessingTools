namespace ProcessingTools.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IExceptionHandler
    {
        Task HandleExceptions(Action action);

        Task HandleExceptions(Func<Task> function);

        Task<T> HandleExceptions<T>(Func<Task<T>> function);
    }
}
