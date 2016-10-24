namespace ProcessingTools.Layout.Processors.Contracts.Taggers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

    using Models.Taggers;

    using ProcessingTools.Contracts;

    public interface IContentTagger
    {
        Task TagContentInDocument(string textToTag, XmlElement tagModel, string xpathTemplate, IDocument document, IContentTaggerSettings settings);

        Task TagContentInDocument(IEnumerable<string> textToTagList, XmlElement tagModel, string xpathTemplate, IDocument document, IContentTaggerSettings settings);

        Task TagContentInDocument(IEnumerable<XmlNode> nodeList, IContentTaggerSettings settings, params XmlElement[] items);
    }
}
