namespace ProcessingTools.Layout.Processors.Contracts.Taggers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Processors.Models.Contracts.Layout;

    public interface ISimpleXmlSerializableObjectTagger<T>
    {
        Task<object> Tag(XmlNode context, XmlNamespaceManager namespaceManager, IEnumerable<T> data, string contentNodesXPath, IContentTaggerSettings settings);
    }
}
