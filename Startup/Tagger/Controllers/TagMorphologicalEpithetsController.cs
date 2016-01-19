namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Bio.Harvesters.Contracts;
    using ProcessingTools.Contracts;

    public class TagMorphologicalEpithetsController : TaggerControllerFactory, ITagMorphologicalEpithetsController
    {
        private IMorphologicalEpithetHarvester harvester;

        public TagMorphologicalEpithetsController(IMorphologicalEpithetHarvester harvester)
        {
            this.harvester = harvester;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            XmlElement tagModel = document.CreateElement("named-content");
            tagModel.SetAttribute("content-type", "morphological epithet");

            var xpathProvider = new XPathProvider(settings.Config);

            var harvestableDocument = new HarvestableDocument(settings.Config, document.OuterXml);
            var data = await this.harvester.Harvest(harvestableDocument.TextContent);

            var tagger = new StringTagger(document.OuterXml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, namespaceManager, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }
    }
}
