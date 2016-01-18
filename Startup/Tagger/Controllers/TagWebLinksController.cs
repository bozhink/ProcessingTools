namespace ProcessingTools.MainProgram.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Attributes;
    using BaseLibrary;
    using Contracts;
    using Harvesters.Contracts;
    using Models;
    using ProcessingTools.Contracts;

    public class TagWebLinksController : ITagWebLinksController
    {
        private const string XPath = "/*";
        private INlmExternalLinksHarvester harvester;
        private XmlDocument document;

        public TagWebLinksController(INlmExternalLinksHarvester harvester)
        {
            this.harvester = harvester;
            this.document = new XmlDocument
            {
                PreserveWhitespace = true
            };
        }

        public async Task Run(XmlNode context, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            this.document.LoadXml(Resources.ContextWrapper);
            this.document.DocumentElement.InnerXml = context.InnerXml;

            var harvestableDocument = new HarvestableDocument(settings.Config, this.document.OuterXml);
            var data = (await this.harvester.Harvest(harvestableDocument.TextContent))
                .Select(i => new ExternalLinkSerializableModel
                {
                    ExternalLinkType = i.Type.GetValue(),
                    Value = i.Content
                });

            var tagger = new SimpleXmlSerializableObjectTagger<ExternalLinkSerializableModel>(this.document.OuterXml, data, XPath, namespaceManager, false, true, logger);

            await tagger.Tag();

            context.InnerXml = tagger.XmlDocument.DocumentElement.InnerXml;
        }
    }
}