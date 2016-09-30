namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Floats;
    using ProcessingTools.Contracts;

    [Description("Tag table foot-notes.")]
    public class TagTableFootnoteController : TaggerControllerFactory, ITagTableFootnoteController
    {
        public TagTableFootnoteController(IDocumentFactory documentFactory)
            : base(documentFactory)
        {
        }

        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            var tagger = new TableFootNotesTagger(document.Xml, logger);

            await tagger.Tag();

            document.Xml = tagger.Xml;
        }
    }
}