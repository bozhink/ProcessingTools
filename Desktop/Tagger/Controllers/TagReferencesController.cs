namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.References;
    using ProcessingTools.Contracts;

    [Description("Tag references.")]
    public class TagReferencesController : TaggerControllerFactory, ITagReferencesController
    {
        private readonly ILogger logger;

        public TagReferencesController(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var tagger = new ReferencesTagger(this.logger);

            tagger.ReferencesGetReferencesXmlPath = settings.ReferencesGetReferencesXmlPath;
            await tagger.Tag(document.XmlDocument.DocumentElement);
        }
    }
}