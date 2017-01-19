namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    [Description("Tag lower taxa.")]
    public class TagLowerTaxaController : GenericDocumentTaggerWithNormalizationController<ILowerTaxaTagger>, ITagLowerTaxaController
    {
        public TagLowerTaxaController(ILowerTaxaTagger tagger, IDocumentNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
