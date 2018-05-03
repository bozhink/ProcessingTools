namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Layout;

    [System.ComponentModel.Description("Tag higher taxa.")]
    public class TagHigherTaxaCommand : DocumentTaggerWithNormalizationCommand<IHigherTaxaTagger>, ITagHigherTaxaCommand
    {
        public TagHigherTaxaCommand(IHigherTaxaTagger tagger, IDocumentSchemaNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
