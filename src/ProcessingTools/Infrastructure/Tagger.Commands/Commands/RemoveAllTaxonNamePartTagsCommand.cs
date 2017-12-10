namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Formatters;

    [System.ComponentModel.Description("Remove all taxon-name-part tags.")]
    public class RemoveAllTaxonNamePartTagsCommand : DocumentFormatterCommand<ITaxonNamePartsRemover>, IRemoveAllTaxonNamePartTagsCommand
    {
        public RemoveAllTaxonNamePartTagsCommand(ITaxonNamePartsRemover formatter)
            : base(formatter)
        {
        }
    }
}
