namespace ProcessingTools.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface ISandbox
    {
        Task Run(Action action);

        Task Run(Func<Task> function);

        Task<T> Run<T>(Func<Task<T>> function);
    }
}
