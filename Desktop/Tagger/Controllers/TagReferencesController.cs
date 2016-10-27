namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.References;

    [Description("Tag references.")]
    public class TagReferencesController : ITagReferencesController
    {
        private readonly IReferencesTagger tagger;

        public TagReferencesController(IReferencesTagger tagger)
        {
            if (tagger == null)
            {
                throw new ArgumentNullException(nameof(tagger));
            }

            this.tagger = tagger;
        }

        public async Task<object> Run(IDocument document, IProgramSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.tagger.ReferencesGetReferencesXmlPath = settings.ReferencesGetReferencesXmlPath;
            return await this.tagger.Tag(document.XmlDocument.DocumentElement);
        }
    }
}