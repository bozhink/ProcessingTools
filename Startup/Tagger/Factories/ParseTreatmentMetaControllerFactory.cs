namespace ProcessingTools.Tagger.Factories
{
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    public abstract class ParseTreatmentMetaControllerFactory : TaggerControllerFactory
    {
        protected abstract ITaxaDataService<ITaxonClassification> Service { get; }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var parser = new TreatmentMetaParser(this.Service, document.OuterXml, logger);

            await parser.Parse();

            document.LoadXml(parser.Xml);
        }
    }
}