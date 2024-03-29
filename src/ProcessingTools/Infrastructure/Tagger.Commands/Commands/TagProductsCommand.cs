﻿namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Products;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag products.")]
    public class TagProductsCommand : GenericDocumentTaggerCommand<IProductsTagger>, ITagProductsCommand
    {
        public TagProductsCommand(IProductsTagger tagger)
            : base(tagger)
        {
        }
    }
}
