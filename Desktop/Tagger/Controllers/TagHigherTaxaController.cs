namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Taggers;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    [Description("Tag higher taxa.")]
    public class TagHigherTaxaController : TaggerControllerFactory, ITagHigherTaxaController
    {
        private readonly IHigherTaxaTagger tagger;
        private readonly IDocumentNormalizer documentNormalizer;

        public TagHigherTaxaController(
            IDocumentFactory documentFactory,
            IHigherTaxaTagger tagger,
            IDocumentNormalizer documentNormalizer)
            : base(documentFactory)
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

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            await this.tagger.Tag(document);
            await this.documentNormalizer.NormalizeToSystem(document);
        }
    }
}
