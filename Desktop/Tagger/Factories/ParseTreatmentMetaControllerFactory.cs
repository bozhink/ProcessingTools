namespace ProcessingTools.Tagger.Factories
{
    using System.Threading.Tasks;

    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    public abstract class ParseTreatmentMetaControllerFactory : TaggerControllerFactory
    {
        public ParseTreatmentMetaControllerFactory(IDocumentFactory documentFactory)
            : base(documentFactory)
        {
        }

        protected abstract ITaxaInformationResolverDataService<ITaxonClassification> Service { get; }

        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            var parser = new TreatmentMetaParser(this.Service, document.Xml, logger);

            await parser.Parse();

            document.Xml = parser.Xml;
        }
    }
}