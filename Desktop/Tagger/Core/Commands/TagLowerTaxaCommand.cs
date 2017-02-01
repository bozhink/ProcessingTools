namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using Processors.Contracts.Processors.Bio.Taxonomy.Taggers;

    [Description("Tag lower taxa.")]
    public class TagLowerTaxaCommand : GenericDocumentTaggerWithNormalizationCommand<ILowerTaxaTagger>, ITagLowerTaxaCommand
    {
        public TagLowerTaxaCommand(ILowerTaxaTagger tagger, IDocumentNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
