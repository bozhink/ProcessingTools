﻿// <copyright file="TagHigherTaxaCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Bio.Taxonomy;
using ProcessingTools.Contracts.Services.Layout;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

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
