namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Floats;

    [Description("Tag floats.")]
    public class TagFloatsController : TaggerControllerFactory, ITagFloatsController
    {
        private readonly ILogger logger;

        public TagFloatsController(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var tagger = new FloatsTagger(this.logger);

            await tagger.Tag(document.XmlDocument.DocumentElement);
        }
    }
}