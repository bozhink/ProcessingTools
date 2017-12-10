namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Taggers;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    public class TagLowerTaxaInItalicCommand : DocumentTaggerWithNormalizationCommand<ILowerTaxaInItalicTagger>, ITagLowerTaxaCommand
    {
        public TagLowerTaxaInItalicCommand(ILowerTaxaInItalicTagger tagger, IDocumentSchemaNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
