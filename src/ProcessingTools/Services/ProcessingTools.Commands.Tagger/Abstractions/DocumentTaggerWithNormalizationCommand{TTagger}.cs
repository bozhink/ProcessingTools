// <copyright file="DocumentTaggerWithNormalizationCommand{TTagger}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Layout;

    /// <summary>
    /// Document tagger with normalization command.
    /// </summary>
    /// <typeparam name="TTagger">Type of tagger.</typeparam>
    public class DocumentTaggerWithNormalizationCommand<TTagger> : ITaggerCommand
        where TTagger : class, IDocumentTagger
    {
        private readonly TTagger tagger;
        private readonly IDocumentSchemaNormalizer documentNormalizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTaggerWithNormalizationCommand{TTagger}"/> class.
        /// </summary>
        /// <param name="tagger">Tagger to be invoked.</param>
        /// <param name="documentNormalizer">Document normalizer.</param>
        public DocumentTaggerWithNormalizationCommand(TTagger tagger, IDocumentSchemaNormalizer documentNormalizer)
        {
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
            this.documentNormalizer = documentNormalizer ?? throw new ArgumentNullException(nameof(documentNormalizer));
        }

        /// <inheritdoc/>
        public async Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var result = await this.tagger.TagAsync(document).ConfigureAwait(false);
            await this.documentNormalizer.NormalizeToSystemAsync(document).ConfigureAwait(false);

            return result;
        }
    }
}
