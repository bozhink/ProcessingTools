namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using Processors.Contracts.Processors.Bio.Taxonomy.Formatters;

    [Description("Remove all taxon-name-part tags.")]
    public class RemoveAllTaxonNamePartTagsCommand : GenericDocumentFormatterCommand<ITaxonNamePartsRemover>, IRemoveAllTaxonNamePartTagsCommand
    {
        public RemoveAllTaxonNamePartTagsCommand(ITaxonNamePartsRemover formatter)
            : base(formatter)
        {
        }
    }
}
