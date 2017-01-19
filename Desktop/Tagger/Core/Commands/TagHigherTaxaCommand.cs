namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    [Description("Tag higher taxa.")]
    public class TagHigherTaxaController : GenericDocumentTaggerWithNormalizationController<IHigherTaxaTagger>, ITagHigherTaxaController
    {
        public TagHigherTaxaController(IHigherTaxaTagger tagger, IDocumentNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
