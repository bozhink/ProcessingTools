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
        private readonly ILogger logger;

        public TagTableFootnoteController(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var tagger = new TableFootNotesTagger(this.logger);

            await tagger.Tag(document.XmlDocument.DocumentElement);
        }
    }
}