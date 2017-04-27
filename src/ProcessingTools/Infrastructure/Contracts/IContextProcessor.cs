namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Processor for context object.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    public interface IContextProcessor<TContext, TResult>
    {
        /// <summary>
        /// Executes processing operation over the context.
        /// </summary>
        /// <param name="context">Context object to be processed.</param>
        /// <returns>Task of result.</returns>
        Task<TResult> Process(TContext context);
    }
}
