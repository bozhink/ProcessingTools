namespace ProcessingTools.MainProgram.Factories
{
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    public abstract class ParseHigherTaxaControllerFactory : TaggerControllerFactory
    {
        protected abstract ITaxaDataService<ITaxonClassification> Service { get; }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var parser = new HigherTaxaParserWithDataService<ITaxonClassification>(document.OuterXml, this.Service, logger);

            await parser.Parse();

            await parser.XmlDocument.PrintNonParsedTaxa(logger);

            document.LoadXml(parser.Xml);
        }
    }
}
