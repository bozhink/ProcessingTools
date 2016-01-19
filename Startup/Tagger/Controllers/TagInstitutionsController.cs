namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;
    using ProcessingTools.Harvesters.Contracts;

    public class TagInstitutionsController : TaggerControllerFactory, ITagInstitutionsController
    {
        private IInstitutionsHarvester harvester;

        public TagInstitutionsController(IInstitutionsHarvester harvester)
        {
            this.harvester = harvester;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            XmlElement tagModel = document.CreateElement("named-content");
            tagModel.SetAttribute("content-type", "institution");

            var xpathProvider = new XPathProvider(settings.Config);

            var harvestableDocument = new HarvestableDocument(settings.Config, document.OuterXml);
            var data = await this.harvester.Harvest(harvestableDocument.TextContent);

            var tagger = new StringTagger(document.OuterXml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, namespaceManager, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }
    }
}
