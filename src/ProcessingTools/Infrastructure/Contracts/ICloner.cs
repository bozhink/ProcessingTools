namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface ICloner<TTarget, TSource, TResult>
    {
        Task<TResult> Clone(TTarget target, TSource source);
    }
}
