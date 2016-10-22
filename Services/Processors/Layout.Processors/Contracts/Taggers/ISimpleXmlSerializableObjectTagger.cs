namespace ProcessingTools.Layout.Processors.Contracts.Taggers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

    public interface ISimpleXmlSerializableObjectTagger<T>
    {
        Task<object> Tag(XmlNode context, XmlNamespaceManager namespaceManager, IEnumerable<T> data, string contentNodesXPath, bool caseSensitive, bool minimalTextSelect);
    }
}
