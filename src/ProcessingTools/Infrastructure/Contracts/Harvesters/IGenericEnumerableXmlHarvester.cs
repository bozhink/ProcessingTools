namespace ProcessingTools.Contracts.Harvesters
{
    using System.Collections.Generic;
    using System.Xml;

    public interface IGenericEnumerableXmlHarvester<T> : IEnumerableHarvester<XmlNode, T>, IGenericXmlHarvester<IEnumerable<T>>
    {
    }
}
