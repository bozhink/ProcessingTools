// <copyright file="TagProductsCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Products;

    /// <summary>
    /// Tag products command.
    /// </summary>
    [System.ComponentModel.Description("Tag products.")]
    public class TagProductsCommand : DocumentTaggerCommand<IProductsTagger>, ITagProductsCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagProductsCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IProductsTagger"/>.</param>
        public TagProductsCommand(IProductsTagger tagger)
            : base(tagger)
        {
        }
    }
}
