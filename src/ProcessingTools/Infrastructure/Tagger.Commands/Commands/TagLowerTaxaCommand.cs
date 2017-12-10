namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Taggers;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    [System.ComponentModel.Description("Tag lower taxa.")]
    public class TagLowerTaxaCommand : DocumentTaggerWithNormalizationCommand<ILowerTaxaTagger>, ITagLowerTaxaCommand
    {
        public TagLowerTaxaCommand(ILowerTaxaTagger tagger, IDocumentSchemaNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
