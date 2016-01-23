namespace ProcessingTools.Data.Miners.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDataMiner<T>
    {
        Task<IQueryable<T>> Mine(string content);
    }
}