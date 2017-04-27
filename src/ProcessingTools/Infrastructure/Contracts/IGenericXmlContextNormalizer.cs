namespace ProcessingTools.Contracts
{
    using System.Xml;

    public interface IGenericXmlContextNormalizer<T> : INormalizer<XmlNode, T>
    {
    }
}
