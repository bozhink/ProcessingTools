namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Processors.Products;

    [Description("Tag products.")]
    public class TagProductsCommand : GenericDocumentTaggerCommand<IProductsTagger>, ITagProductsCommand
    {
        public TagProductsCommand(IProductsTagger tagger)
            : base(tagger)
        {
        }
    }
}
