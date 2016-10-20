namespace ProcessingTools.Tagger.Factories
{
    using System.Threading.Tasks;

    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    public abstract class ParseTreatmentMetaControllerFactory : TaggerControllerFactory
    {
        private readonly ILogger logger;

        public ParseTreatmentMetaControllerFactory(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
        }

        protected abstract ITaxaInformationResolverDataService<ITaxonClassification> Service { get; }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var parser = new TreatmentMetaParser<ITaxaInformationResolverDataService<ITaxonClassification>>(this.Service, this.logger);

            await parser.Parse(document);
        }
    }
}