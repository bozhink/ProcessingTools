namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Processor for context object.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object.</typeparam>
    public interface IContextProcessor<TContext>
    {
        /// <summary>
        /// Executes processing operation over the context.
        /// </summary>
        /// <param name="context">Context object to be processed.</param>
        /// <returns>Task.</returns>
        Task Process(TContext context);
    }
}
