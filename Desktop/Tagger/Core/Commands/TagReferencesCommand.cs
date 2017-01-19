namespace ProcessingTools.Tagger.Commands
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
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

        public Task<object> Run(IDocument document, IProgramSettings settings)
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
            return this.tagger.Tag(document.XmlDocument.DocumentElement);
        }
    }
}
