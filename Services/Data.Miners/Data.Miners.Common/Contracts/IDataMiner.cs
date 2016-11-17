namespace ProcessingTools.Data.Miners.Common.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDataMiner<T>
    {
        Task<IEnumerable<T>> Mine(string content);
    }
}