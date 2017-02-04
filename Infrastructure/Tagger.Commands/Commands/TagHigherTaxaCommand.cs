namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using Processors.Contracts.Processors.Bio.Taxonomy.Taggers;

    [Description("Tag higher taxa.")]
    public class TagHigherTaxaCommand : GenericDocumentTaggerWithNormalizationCommand<IHigherTaxaTagger>, ITagHigherTaxaCommand
    {
        public TagHigherTaxaCommand(IHigherTaxaTagger tagger, IDocumentNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
