namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Formatters;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Remove all taxon-name-part tags.")]
    public class RemoveAllTaxonNamePartTagsCommand : GenericDocumentFormatterCommand<ITaxonNamePartsRemover>, IRemoveAllTaxonNamePartTagsCommand
    {
        public RemoveAllTaxonNamePartTagsCommand(ITaxonNamePartsRemover formatter)
            : base(formatter)
        {
        }
    }
}
