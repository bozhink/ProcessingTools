namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
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
