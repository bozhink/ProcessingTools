namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Contracts;

    [Description("Parse lower taxa.")]
    public class ParseLowerTaxaController : TaggerControllerFactory, IParseLowerTaxaController
    {
        private readonly ILogger logger;

        public ParseLowerTaxaController(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var parser = new LowerTaxaParser(this.logger);

            await parser.Parse(document.XmlDocument.DocumentElement);
        }
    }
}
