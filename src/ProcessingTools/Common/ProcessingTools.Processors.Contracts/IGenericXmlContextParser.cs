namespace ProcessingTools.Contracts
{
    using System.Xml;

    public interface IGenericXmlContextParser<T> : IContextParser<XmlNode, T>
    {
    }
}
