namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.References;
    using ProcessingTools.Contracts;

    [Description("Parse references.")]
    public class ParseReferencesController : TaggerControllerFactory, IParseReferencesController
    {
        private readonly ILogger logger;

        public ParseReferencesController(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var parser = new ReferencesParser(document.Xml, this.logger);

            await parser.Parse();

            document.Xml = parser.Xml;
        }
    }
}
