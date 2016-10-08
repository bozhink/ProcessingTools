namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;

    [Description("Tag table foot-notes.")]
    public class TagTableFootnoteController : TaggerControllerFactory, ITagTableFootnoteController
    {
        private readonly ITableFootNotesTagger tagger;

        public TagTableFootnoteController(IDocumentFactory documentFactory, ITableFootNotesTagger tagger)
            : base(documentFactory)
        {
            if (tagger == null)
            {
                throw new ArgumentNullException(nameof(tagger));
            }

            this.tagger = tagger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            await this.tagger.Tag(document.XmlDocument.DocumentElement);
        }
    }
}