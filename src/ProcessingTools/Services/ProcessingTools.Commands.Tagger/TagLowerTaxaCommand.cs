﻿// <copyright file="TagLowerTaxaCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Layout;

    /// <summary>
    /// Tag lower taxa command.
    /// </summary>
    [System.ComponentModel.Description("Tag lower taxa.")]
    public class TagLowerTaxaCommand : DocumentTaggerWithNormalizationCommand<ILowerTaxaTagger>, ITagLowerTaxaCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagLowerTaxaCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="ILowerTaxaTagger"/>.</param>
        /// <param name="documentNormalizer">Instance of <see cref="IDocumentSchemaNormalizer"/>.</param>
        public TagLowerTaxaCommand(ILowerTaxaTagger tagger, IDocumentSchemaNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}