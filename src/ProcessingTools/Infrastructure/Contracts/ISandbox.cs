namespace ProcessingTools.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface ISandbox
    {
        Task RunAsync(Action action);

        Task RunAsync(Func<Task> function);

        Task<T> RunAsync<T>(Func<Task<T>> function);
    }
}
