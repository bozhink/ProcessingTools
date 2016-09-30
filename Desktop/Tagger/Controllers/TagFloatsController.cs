namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Floats;
    using ProcessingTools.Contracts;

    [Description("Tag floats.")]
    public class TagFloatsController : TaggerControllerFactory, ITagFloatsController
    {
        public TagFloatsController(IDocumentFactory documentFactory)
            : base(documentFactory)
        {
        }

        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            var tagger = new FloatsTagger(document.Xml, logger);

            await tagger.Tag();

            document.Xml = tagger.Xml;
        }
    }
}