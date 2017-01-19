namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    [Description("Tag higher taxa.")]
    public class TagHigherTaxaCommand : GenericDocumentTaggerWithNormalizationCommand<IHigherTaxaTagger>, ITagHigherTaxaCommand
    {
        public TagHigherTaxaCommand(IHigherTaxaTagger tagger, IDocumentNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
