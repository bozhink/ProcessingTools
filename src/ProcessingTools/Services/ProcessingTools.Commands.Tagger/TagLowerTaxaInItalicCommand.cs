// <copyright file="TagLowerTaxaInItalicCommand.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Layout;

    /// <summary>
    /// Tag lower taxa in italic command.
    /// </summary>
    public class TagLowerTaxaInItalicCommand : DocumentTaggerWithNormalizationCommand<ILowerTaxaInItalicTagger>, ITagLowerTaxaCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagLowerTaxaInItalicCommand"/> class.
        /// </summary>
        /// <param name="tagger">Instance of <see cref="ILowerTaxaInItalicTagger"/>.</param>
        /// <param name="documentNormalizer">Instance of <see cref="IDocumentSchemaNormalizer"/>.</param>
        public TagLowerTaxaInItalicCommand(ILowerTaxaInItalicTagger tagger, IDocumentSchemaNormalizer documentNormalizer)
            : base(tagger, documentNormalizer)
        {
        }
    }
}
