namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Layout;

    [System.ComponentModel.Description("Tag lower taxa.")]
    public class TagLowerTaxaCommand : DocumentTaggerWithNormalizationCommand<ILowerTaxaTagger>, ITagLowerTaxaCommand
    {
        public TagLowerTaxaCommand(ILowerTaxaTagger tagger, IDocumentSchemaNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
