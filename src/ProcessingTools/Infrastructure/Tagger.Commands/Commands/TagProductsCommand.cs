﻿namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Products;

    [System.ComponentModel.Description("Tag products.")]
    public class TagProductsCommand : DocumentTaggerCommand<IProductsTagger>, ITagProductsCommand
    {
        public TagProductsCommand(IProductsTagger tagger)
            : base(tagger)
        {
        }
    }
}
