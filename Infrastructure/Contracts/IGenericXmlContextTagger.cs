namespace ProcessingTools.Contracts
{
    using System.Xml;

    public interface IGenericXmlContextTagger<T> : IContextTagger<XmlNode, T>
    {
    }
}
