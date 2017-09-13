namespace ProcessingTools.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IExceptionHandler
    {
        Task HandleExceptionsAsync(Action action);

        Task HandleExceptionsAsync(Func<Task> function);

        Task<T> HandleExceptionsAsync<T>(Func<Task<T>> function);
    }
}
