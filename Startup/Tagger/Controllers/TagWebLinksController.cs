namespace ProcessingTools.MainProgram.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Attributes;
    using BaseLibrary;
    using Contracts;
    using Factories;
    using Harvesters.Contracts;
    using Models;
    using ProcessingTools.Contracts;

    public class TagWebLinksController : TaggerControllerFactory, ITagWebLinksController
    {
        private const string XPath = "/*";
        private INlmExternalLinksHarvester harvester;

        public TagWebLinksController(INlmExternalLinksHarvester harvester)
        {
            this.harvester = harvester;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var harvestableDocument = new HarvestableDocument(settings.Config, document.OuterXml);
            var data = (await this.harvester.Harvest(harvestableDocument.TextContent))
                .Select(i => new ExternalLinkSerializableModel
                {
                    ExternalLinkType = i.Type.GetValue(),
                    Value = i.Content
                });

            var tagger = new SimpleXmlSerializableObjectTagger<ExternalLinkSerializableModel>(document.OuterXml, data, XPath, namespaceManager, false, true, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }
    }
}
