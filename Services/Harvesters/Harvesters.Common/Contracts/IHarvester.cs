namespace ProcessingTools.Harvesters.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    public interface IHarvester<T>
    {
        Task<IQueryable<T>> Harvest(XmlNode context);
    }
}