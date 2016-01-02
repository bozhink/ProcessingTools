namespace ProcessingTools.BaseLibrary.Uri
{
    using System.Linq;
    using System.Xml;

    using Attributes;
    using Configurator;
    using Contracts;
    using Harvesters.Contracts;
    using ProcessingTools.Contracts.Log;

    public class NlmExternalLinksTagger : HarvestableDocument, IBaseTagger
    {
        private const string SelectNodesToTagExternalLinksXPathTemplate = "/*";

        private INlmExternalLinksHarvester harvester;
        private ILogger logger;

        public NlmExternalLinksTagger(Config config, string xml, INlmExternalLinksHarvester harvester, ILogger logger)
            : base(config, xml)
        {
            this.harvester = harvester;
            this.logger = logger;
        }

        public NlmExternalLinksTagger(IBase baseObject, INlmExternalLinksHarvester harvester, ILogger logger)
            : base(baseObject)
        {
            this.harvester = harvester;
            this.logger = logger;
        }

        public void Tag()
        {
            this.harvester.Harvest(this.TextContent);

            var items = this.harvester.Data.ToList().OrderByDescending(i => i.Content.Length);

            foreach (var item in items)
            {
                XmlElement tag = this.XmlDocument.CreateElement("ext-link");
                tag.SetAttribute("ext-link-type", item.Type.GetValue());

                tag.InnerXml = item.Content;
                tag.TagContentInDocument(
                    SelectNodesToTagExternalLinksXPathTemplate,
                    this.XmlDocument,
                    false,
                    true,
                    this.logger);
            }
        }
    }
}