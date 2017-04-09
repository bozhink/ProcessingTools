namespace ProcessingTools.Contracts.Data.Miners
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITextDataMiner<TContext, T>
    {
        Task<IEnumerable<T>> Mine(TContext context, IEnumerable<string> seed, IEnumerable<string> stopWords);
    }
}
