namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface ICloner<TContext, TContent, TResult>
    {
        Task<TResult> Clone(TContext context, TContent content);
    }
}
