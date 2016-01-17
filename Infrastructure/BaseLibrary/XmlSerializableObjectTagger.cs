namespace ProcessingTools.BaseLibrary
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Factories;
    using ProcessingTools.Contracts.Log;

    public class XmlSerializableObjectTagger<T> : XmlSerializableObjectTaggerFactory<T>
    {
        private string contentNodesXPathTemplate;
        private XmlNamespaceManager namespaceManager;
        private IQueryable<T> data;
        private ILogger logger;
        private bool caseSensitive;
        private bool minimalTextSelect;

        public XmlSerializableObjectTagger(string xml, IQueryable<T> data, string contentNodesXPathTemplate, XmlNamespaceManager namespaceManager, bool caseSensitive, bool minimalTextSelect, ILogger logger)
            : base(xml)
        {
            this.data = data;
            this.contentNodesXPathTemplate = contentNodesXPathTemplate;
            this.namespaceManager = namespaceManager;
            this.logger = logger;
            this.caseSensitive = caseSensitive;
            this.minimalTextSelect = minimalTextSelect;
        }

        public override Task Tag()
        {
            var items = this.data.ToList()
                .Select(this.SerializeObject)
                .OrderByDescending(i => i.InnerText.Length);

            return Task.Run(() =>
            {
                foreach (var item in items)
                {
                    item.TagContentInDocument(
                        this.contentNodesXPathTemplate,
                        this.namespaceManager,
                        this.XmlDocument,
                        this.caseSensitive,
                        this.minimalTextSelect,
                        this.logger)
                        .Wait();
                }
            });
        }
    }
}