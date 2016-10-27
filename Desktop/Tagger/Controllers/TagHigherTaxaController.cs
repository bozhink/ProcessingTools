namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Taggers;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    [Description("Tag higher taxa.")]
    public class TagHigherTaxaController : ITagHigherTaxaController
    {
        private readonly IHigherTaxaTagger tagger;
        private readonly IDocumentNormalizer documentNormalizer;

        public TagHigherTaxaController(IHigherTaxaTagger tagger, IDocumentNormalizer documentNormalizer)
        {
            if (tagger == null)
            {
                throw new ArgumentNullException(nameof(tagger));
            }

            if (documentNormalizer == null)
            {
                throw new ArgumentNullException(nameof(documentNormalizer));
            }

            this.tagger = tagger;
            this.documentNormalizer = documentNormalizer;
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

            var result = await this.tagger.Tag(document);
            await this.documentNormalizer.NormalizeToSystem(document);

            return result;
        }
    }
}
