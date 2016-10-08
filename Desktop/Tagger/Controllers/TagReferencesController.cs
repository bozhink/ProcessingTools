namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.References;

    [Description("Tag references.")]
    public class TagReferencesController : TaggerControllerFactory, ITagReferencesController
    {
        private readonly IReferencesTagger tagger;

        public TagReferencesController(IDocumentFactory documentFactory, IReferencesTagger tagger)
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
            this.tagger.ReferencesGetReferencesXmlPath = settings.ReferencesGetReferencesXmlPath;
            await this.tagger.Tag(document.XmlDocument.DocumentElement);
        }
    }
}