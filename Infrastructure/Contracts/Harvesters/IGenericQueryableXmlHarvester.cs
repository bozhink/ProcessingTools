namespace ProcessingTools.Contracts.Harvesters
{
    using System.Linq;
    using System.Xml;

    public interface IGenericQueryableXmlHarvester<T> : IQueryableHarvester<XmlNode, T>, IGenericXmlHarvester<IQueryable<T>>
    {
    }
}
