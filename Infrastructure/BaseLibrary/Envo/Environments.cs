namespace ProcessingTools.BaseLibrary
{
    using System.Linq;
    using System.Xml;

    using Bio.Harvesters.Contracts;
    using Configurator;
    using Contracts;
    using Models;
    using ProcessingTools.Contracts.Log;

    public class Environments : HarvestableDocument, IBaseTagger
    {
        private const string EnvoTagName = "envo";
        private IEnvoTermsHarvester harvester;
        private ILogger logger;

        public Environments(Config config, string xml, IEnvoTermsHarvester harvester, ILogger logger)
            : base(config, xml)
        {
            this.harvester = harvester;
            this.logger = logger;
        }

        public void Tag()
        {
            const string XPath = "/*";
            XmlNodeList nodeList = this.XmlDocument.SelectNodes(XPath, this.NamespaceManager);

            var data = this.harvester.Harvest(this.TextContent).Result;
            var terms = data
                .Select(t => new EnvoTermResponseModel
                {
                    EntityId = t.EntityId,
                    EnvoId = t.EnvoId,
                    Content = t.Content
                })
                .ToList();

            foreach (var term in terms)
            {
                XmlElement element = this.XmlDocument.CreateElement(EnvoTagName);
                element.InnerText = term.Content;

                XmlAttribute entityIdAttribute = element.OwnerDocument.CreateAttribute("ID");
                entityIdAttribute.InnerText = term.EntityId;
                element.Attributes.Append(entityIdAttribute);

                XmlAttribute envoIdAttribute = element.OwnerDocument.CreateAttribute("EnvoID");
                envoIdAttribute.InnerText = term.EnvoId;
                element.Attributes.Append(envoIdAttribute);

                XmlAttribute contentAttribute = element.OwnerDocument.CreateAttribute("VerbatimTerm");
                contentAttribute.InnerText = term.Content;
                element.Attributes.Append(contentAttribute);

                element.TagContentInDocument(nodeList, false, false, this.logger);
            }
        }
    }
}