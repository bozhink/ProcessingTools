namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface IValidator<TContext, TResult>
    {
        Task<TResult> Validate(TContext context);
    }
}