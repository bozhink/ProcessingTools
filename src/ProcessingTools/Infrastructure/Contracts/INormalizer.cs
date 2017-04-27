namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface INormalizer<TContext, TResult> : ITransformer
    {
        Task<TResult> Normalize(TContext context);
    }
}
