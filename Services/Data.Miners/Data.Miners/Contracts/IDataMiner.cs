namespace ProcessingTools.Data.Miners.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDataMiner<T>
    {
        Task<IEnumerable<T>> Mine(string content);
    }
}