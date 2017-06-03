namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Taggers;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    public class TagLowerTaxaInItalicCommand : GenericDocumentTaggerWithNormalizationCommand<ILowerTaxaInItalicTagger>, ITagLowerTaxaCommand
    {
        public TagLowerTaxaInItalicCommand(ILowerTaxaInItalicTagger tagger, IDocumentSchemaNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
