namespace ProcessingTools.Harvesters.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IHarvester<T>
    {
        Task<IQueryable<T>> Harvest(string content);
    }
}