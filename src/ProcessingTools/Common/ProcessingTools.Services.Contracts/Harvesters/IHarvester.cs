namespace ProcessingTools.Contracts.Harvesters
{
    using System.Threading.Tasks;

    public interface IHarvester<TContext, TResult>
    {
        Task<TResult> Harvest(TContext context);
    }
}
