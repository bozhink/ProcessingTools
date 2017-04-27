namespace ProcessingTools.Contracts.Harvesters
{
    using System.Xml;

    public interface IGenericXmlHarvester<T> : IHarvester<XmlNode, T>
    {
    }
}
