// <copyright file="TagHigherTaxaCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Layout;

    /// <summary>
    /// Tag higher taxa command.
    /// </summary>
    [System.ComponentModel.Description("Tag higher taxa.")]
    public class TagHigherTaxaCommand : DocumentTaggerWithNormalizationCommand<IHigherTaxaTagger>, ITagHigherTaxaCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagHigherTaxaCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="IHigherTaxaTagger"/>.</param>
        /// <param name="documentNormalizer">Instance of <see cref="IDocumentSchemaNormalizer"/>.</param>
        public TagHigherTaxaCommand(IHigherTaxaTagger tagger, IDocumentSchemaNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
