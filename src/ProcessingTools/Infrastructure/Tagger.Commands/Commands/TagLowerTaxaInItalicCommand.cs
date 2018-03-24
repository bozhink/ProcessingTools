namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Layout;

    public class TagLowerTaxaInItalicCommand : DocumentTaggerWithNormalizationCommand<ILowerTaxaInItalicTagger>, ITagLowerTaxaCommand
    {
        public TagLowerTaxaInItalicCommand(ILowerTaxaInItalicTagger tagger, IDocumentSchemaNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
