namespace ProcessingTools.Harvesters.Common.Contracts
{
    using System.Linq;

    public interface IHarvester<T>
    {
        IQueryable<T> Data { get; }

        void Harvest(string content);
    }
}
