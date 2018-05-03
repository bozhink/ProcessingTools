namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Products;

    [System.ComponentModel.Description("Tag products.")]
    public class TagProductsCommand : DocumentTaggerCommand<IProductsTagger>, ITagProductsCommand
    {
        public TagProductsCommand(IProductsTagger tagger)
            : base(tagger)
        {
        }
    }
}
