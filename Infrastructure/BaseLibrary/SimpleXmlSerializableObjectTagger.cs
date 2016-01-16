namespace ProcessingTools.BaseLibrary
{
    using System.Linq;
    using System.Xml;

    using Factories;
    using ProcessingTools.Contracts.Log;

    public class SimpleXmlSerializableObjectTagger<T> : XmlSerializableObjectTaggerFactory<T>
    {
        private string contentNodesXPath;
        private XmlNamespaceManager namespaceManager;
        private IQueryable<T> data;
        private ILogger logger;
        private bool caseSensitive;
        private bool minimalTextSelect;

        public SimpleXmlSerializableObjectTagger(string xml, IQueryable<T> data, string contentNodesXPath, XmlNamespaceManager namespaceManager, bool caseSensitive, bool minimalTextSelect, ILogger logger)
            : base(xml)
        {
            this.data = data;
            this.contentNodesXPath = contentNodesXPath;
            this.namespaceManager = namespaceManager;
            this.logger = logger;
            this.caseSensitive = caseSensitive;
            this.minimalTextSelect = minimalTextSelect;
        }

        public override void Tag()
        {
            XmlNodeList nodeList = this.XmlDocument.SelectNodes(this.contentNodesXPath, this.namespaceManager);

            this.data.ToList()
                .Select(this.SerializeObject)
                .OrderByDescending(i => i.InnerText.Length)
                .TagContentInDocument(nodeList, this.caseSensitive, this.minimalTextSelect, this.logger);
        }
    }
}
