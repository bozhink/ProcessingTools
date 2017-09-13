namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface IValidator<TContext, TResult>
    {
        Task<TResult> ValidateAsync(TContext context, IReporter reporter);
    }
}