namespace ProcessingTools.Layout.Processors.Contracts.Taggers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts;

    public interface IContentTagger
    {
        Task TagContentInDocument(string textToTag, XmlElement tagModel, string xpathTemplate, IDocument document, bool caseSensitive = true, bool minimalTextSelect = false);

        Task TagContentInDocument(IEnumerable<string> textToTagList, XmlElement tagModel, string xpathTemplate, IDocument document, bool caseSensitive = true, bool minimalTextSelect = false);

        Task TagContentInDocument(IEnumerable<XmlNode> nodeList, bool caseSensitive, bool minimalTextSelect, params XmlElement[] items);
    }
}
