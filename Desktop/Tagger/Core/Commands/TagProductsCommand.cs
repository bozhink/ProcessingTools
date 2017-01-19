namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Products;

    [Description("Tag products.")]
    public class TagProductsController : GenericDocumentTaggerController<IProductsTagger>, ITagProductsController
    {
        public TagProductsController(IProductsTagger tagger)
            : base(tagger)
        {
        }
    }
}
