namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Formatters;

    [Description("Remove all taxon-name-part tags.")]
    public class RemoveAllTaxonNamePartTagsController : GenericDocumentFormatterController<ITaxonNamePartsRemover>, IRemoveAllTaxonNamePartTagsController
    {
        public RemoveAllTaxonNamePartTagsController(ITaxonNamePartsRemover formatter)
            : base(formatter)
        {
        }
    }
}
