namespace ProcessingTools.BaseLibrary
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Factories;
    using ProcessingTools.Contracts;

    public class SimpleXmlSerializableObjectTagger<T> : XmlSerializableObjectTaggerFactory<T>
    {
        private string contentNodesXPath;
        private XmlNamespaceManager namespaceManager;
        private IQueryable<T> data;
        private ILogger logger;
        private bool caseSensitive;
        private bool minimalTextSelect;

        public SimpleXmlSerializableObjectTagger(string xml, IQueryable<T> data, string contentNodesXPath, XmlNamespaceManager namespaceManager, bool caseSensitive, bool minimalTextSelect, ILogger logger)
            : base(xml, namespaceManager)
        {
            this.data = data;
            this.contentNodesXPath = contentNodesXPath;
            this.namespaceManager = namespaceManager;
            this.logger = logger;
            this.caseSensitive = caseSensitive;
            this.minimalTextSelect = minimalTextSelect;
        }

        public override Task Tag()
        {
            XmlNodeList nodeList = this.XmlDocument.SelectNodes(this.contentNodesXPath, this.namespaceManager);

            var items = this.data.ToList()
                .Select(this.SerializeObject)
                .OrderByDescending(i => i.InnerText.Length);

            return Task.Run(() =>
            {
                foreach (var item in items)
                {
                    item.TagContentInDocument(
                        nodeList, 
                        this.caseSensitive, 
                        this.minimalTextSelect, 
                        this.logger)
                        .Wait();
                }
            });
        }
    }
}
