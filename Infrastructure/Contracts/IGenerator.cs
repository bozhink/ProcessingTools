namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface IGenerator<TContext, TResult>
    {
        Task<TResult> Generate(TContext context);
    }
}
