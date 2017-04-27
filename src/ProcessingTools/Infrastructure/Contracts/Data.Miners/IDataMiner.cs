namespace ProcessingTools.Contracts.Data.Miners
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDataMiner<TContext, T>
    {
        Task<IEnumerable<T>> Mine(TContext context);
    }
}
